using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCartByUserIdAsync(Guid userId);
        Task<CartItem> GetCartItemAsync(Guid cartId, Guid productId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(CartItem cartItem);
        Task ClearCartAsync(Guid cartId);
        Task<int> GetCartItemCountAsync(Guid userId);
        Task CreateCartAsync(ShoppingCart cart);

        Task<ShoppingCart> GetCartByUserIdWithNoTrackingAsync(Guid userId);
    }
        
}