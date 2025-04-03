using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.Constants.ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
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
                    CreatedAt = DateTime.UtcNow
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
                    AddedAt = DateTime.UtcNow
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
            var cart = await GetCartAsync(userId);
            if (!cart.Items.Any())
            {
                throw new ValidationException("Cart is empty");
            }

            // Create order logic here
            // This is a simplified version - you'll need to expand this
            var order = new Order
            {
                UserId = userId,
                Status = OrderStatusEnum.Pending,
                TotalAmount = cart.TotalAmount,
                ShippingPostalCode = "TODO", // Get from user address
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear the cart after order is placed
            await ClearCartAsync(userId);

            return true;
        }
    }
}