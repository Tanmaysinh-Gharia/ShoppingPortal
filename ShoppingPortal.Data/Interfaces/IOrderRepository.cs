using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Helpers;
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
        //Task<PaginatedResult<Order>> GetUserOrdersPaginatedAsync(Guid userId, int page, int pageSize);
        //Task<OrderWithItemsResult> GetOrderWithItemsAsync(Guid userId, Guid orderId);
        //Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusEnum status);
        //Task<bool> RestoreProductQuantitiesAsync(Guid orderId);

        //Display Order
        Task<PaginatedResult<Order>> GetUserOrdersPaginatedAsync(Guid userId, int page, int pageSize);

        //Placing order
        Task RemoveItemsFromCartAndAddToOrder(Guid cartId, ICollection<CartItem> cartItems, ICollection<OrderItem> orderItems);

        //Cancel order
        Task<Order> GetOrderWithItemsAsync(Guid orderId);
        Task<bool> CancelOrderTransactionAsync(Guid orderId, OrderStatusEnum newStatus);


        

    }
}
