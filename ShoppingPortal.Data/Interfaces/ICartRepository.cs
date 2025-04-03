using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCartByUserIdAsync(Guid userId);
        Task<CartItem> GetCartItemAsync(Guid cartId, Guid productId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task<bool> SaveChangesAsync();
    }
}
