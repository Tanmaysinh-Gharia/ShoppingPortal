using ShoppingPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(Guid userId);
        Task AddToCartAsync(Guid userId, AddToCartDto addToCartDto);
        Task UpdateCartItemAsync(Guid userId, UpdateQuantityDto updateQuantityDto);
        Task RemoveFromCartAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
        Task<int> GetCartItemCountAsync(Guid userId);
        Task<bool> PlaceOrderAsync(Guid userId);
    }
}
