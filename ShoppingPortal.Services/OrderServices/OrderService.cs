using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Exceptions;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepo;
        private readonly IUserRepository _userRepo;
        public OrderService(
          ApplicationDbContext context,
          ICartRepository cartRepository,
          IProductRepository productRepository,
          IMapper mapper,
          IOrderRepository orderRepo,
          IUserRepository userRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _orderRepo = orderRepo;
            _userRepo = userRepository;
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
                string shippingPostalCode =  await _userRepo.GetPostalCodeOfUser(userId);
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
                    }).ToList(),
                    ShippingPostalCode = shippingPostalCode
                };

                //Adding to database    
                _context.Orders.Add(order);
                //_context.OrderItems.AddRange(order.OrderItems); // -> This will add without any concurrancy check of row version

                // 4. Concurrancy check Of Products
                foreach (OrderItem item in order.OrderItems)
                {
                    var currentProduct = await _context.Products
                        .Where(p => p.ProductId == item.ProductId)
                        .Select(p => new { p.Name, p.StockQuantity, p.RowVersion })
                        .FirstOrDefaultAsync();

                    if (!StructuralComparisons.StructuralEqualityComparer
                        .Equals(productVersions[item.ProductId], currentProduct.RowVersion))
                    {
                        throw new DbUpdateConcurrencyException($"Product {item.ProductId} was modified concurrently");
                    }
                    if (currentProduct.StockQuantity < item.Quantity)
                    {
                        throw new InsufficientStockException(currentProduct.Name, currentProduct.StockQuantity);
                    }
                    Product product = await _context.Products.FindAsync(item.ProductId);
                    product.StockQuantity -= item.Quantity;
                }

                await _orderRepo.RemoveItemsFromCartAndAddToOrder(cart.CartId, cart.CartItems, order.OrderItems);

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
