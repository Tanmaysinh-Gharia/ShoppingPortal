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
    }
}
