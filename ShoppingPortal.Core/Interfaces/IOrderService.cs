using ShoppingPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Interfaces
{
    public interface IOrderService
    {
        Task<bool> PlaceOrderAsync(Guid userId);
        //async Task<bool> CancelOrderAsync(Guid orderId);

        //Task<List<OrderItemWithProduct>> GetOrderItemsWithStatusAsync(Guid orderId);
        Task<List<OrderItemStatusDto>> GetOrderItemStatusesAsync(Guid userId, Guid orderId);
    }
}
