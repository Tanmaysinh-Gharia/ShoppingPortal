using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RemoveItemsFromCartAndAddToOrder(Guid cartId, ICollection<CartItem> cartItems, ICollection<OrderItem> orderItems)
        {
            // Implementation for placing an order
            // This is just a placeholder. Actual implementation will depend on your business logic.
            _context.OrderItems.AddRange(orderItems);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }


        //Orders Display Procedures
        public async Task<PaginatedResult<Order>> GetUserOrdersPaginatedAsync(Guid userId, int page, int pageSize)
        {
            var query = _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Order>(items, totalCount, page, pageSize);
        }

        //Cancle Order Procudures
        public async Task<Order> GetOrderWithItemsAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<bool> CancelOrderTransactionAsync(Guid orderId, OrderStatusEnum newStatus)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Get order with items and products
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null) return false;

                // 2. Restore all product quantities
                foreach (var item in order.OrderItems)
                {
                    if (item.Product != null)
                    {
                        item.Product.StockQuantity += item.Quantity;
                    }
                }

                // 3. Update order status
                order.Status = newStatus;
                order.UpdatedAt = DateTime.UtcNow;

                // 4. Save all changes atomically
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<int> GetOrderCountAsync()
        {
            int count  = await _context.Orders.CountAsync();
            return count;
        }
    }
}
