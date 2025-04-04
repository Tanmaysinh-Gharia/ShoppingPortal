using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task RemoveItemsFromCartAndAddToOrder(Guid cartId, ICollection<CartItem> cartItems, ICollection<OrderItem> orderItems);
    }
}
