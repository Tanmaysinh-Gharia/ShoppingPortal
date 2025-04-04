using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.Constants.ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Exceptions;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CartService(
            ApplicationDbContext context,
            ICartRepository cartRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _context = context;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartAsync(Guid userId)
        {
            var cart = await _context.ShoppingCarts
            .Include(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                return new CartDto
                {
                    UserId = userId,
                    Items = new List<CartItemDto>(),
                    TotalAmount = 0
                };
            }

            var cartDto = new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                CreatedAt = cart.CreatedAt,
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity,
                    ImageUrl = AppConstants.ImageBasePath + $"/{ci.ProductId}.webp" // Or your image URL logic
                }).ToList(),
                TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
            };

            return cartDto;
        }

        public async Task AddToCartAsync(Guid userId, AddToCartDto addToCartDto)
        {
            var product = await _productRepository.GetByIdAsync(addToCartDto.ProductId);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            if (addToCartDto.Quantity < 1 || addToCartDto.Quantity > ProductConstants.MaxQuantityPerProduct)
            {
                throw new ValidationException("Invalid quantity");
            }

            var cart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.ShoppingCarts.Add(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == addToCartDto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity = Math.Min(
                    existingItem.Quantity + addToCartDto.Quantity,
                    ProductConstants.MaxQuantityPerProduct);
            }
            else
            {
                if (cart.CartItems.Count >= ProductConstants.MaxItemsInCart)
                {
                    throw new ValidationException("Cart item limit reached");
                }

                cart.CartItems.Add(new CartItem
                {
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    AddedAt = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(Guid userId, UpdateQuantityDto updateQuantityDto)
        {
            var cart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                throw new NotFoundException("Cart not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == updateQuantityDto.ProductId);
            if (cartItem == null)
            {
                throw new NotFoundException("Item not found in cart");
            }

            if (updateQuantityDto.Quantity < 1 || updateQuantityDto.Quantity > ProductConstants.MaxQuantityPerProduct)
            {
                throw new ValidationException("Invalid quantity");
            }

            cartItem.Quantity = updateQuantityDto.Quantity;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(Guid userId, Guid productId)
        {
            var cart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                throw new NotFoundException("Cart not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCartItemCountAsync(Guid userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            return cart?.CartItems.Sum(ci => ci.Quantity) ?? 0;
        }

        public async Task<bool> PlaceOrderAsync(Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
            try
            {
                // 1. Get cart with items (no tracking for manual attach) -- || Eager Loading
                ShoppingCart cart = await _cartRepository.GetCartByUserIdWithNoTrackingAsync(userId);

                if (cart == null || !cart.CartItems.Any())
                    throw new ValidationException("Cart is empty");

                // 2. Attach Rowversion and check
                Dictionary<Guid, byte[]> productVersions = new Dictionary<Guid, byte[]>();

                foreach (CartItem item in cart.CartItems)
                {
                    _context.Products.Attach(item.Product);
                    productVersions.Add(item.ProductId, item.Product.RowVersion);
                }

                // 3. Create Order
                Guid newOrderID = Guid.NewGuid();
                Order order = new Order
                {
                    UserId = userId,
                    OrderId = newOrderID,
                    CreatedAt = DateTime.Now,
                    Status = OrderStatusEnum.Pending,
                    TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
                    OrderItems = cart.CartItems.Select(ci => new OrderItem
                    {
                        OrderItemId = Guid.NewGuid(),
                        OrderId = newOrderID,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        UnitPrice = ci.Product.Price
                    }).ToList()
                };

                //Adding to database    
                _context.Orders.Add(order);
                //_context.OrderItems.AddRange(order.OrderItems); // -> This will add without any concurrancy check of row version

                // 4. Concurrancy check Of Products
                foreach (OrderItem item in order.OrderItems)
                {
                    var currentProduct = await _context.Products
                        .Where(p => p.ProductId == item.ProductId)
                        .Select(p => new { p.Name,p.StockQuantity, p.RowVersion })
                        .FirstOrDefaultAsync();

                    if (!StructuralComparisons.StructuralEqualityComparer
                        .Equals(productVersions[item.ProductId], currentProduct.RowVersion))
                    {
                        throw new DbUpdateConcurrencyException($"Product {item.ProductId} was modified concurrently");
                    }
                    if (currentProduct.StockQuantity < item.Quantity)
                    {
                        throw new InsufficientStockException(currentProduct.Name,currentProduct.StockQuantity);
                    }
                    _context.OrderItems.Add(item);
                }

                // 5. Clear Cart and Add to Orders
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }
    }
}