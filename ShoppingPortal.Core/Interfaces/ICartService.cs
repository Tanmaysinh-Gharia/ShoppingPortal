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
        Task<bool> AddToCartAsync(AddToCartDto addToCartDto, Guid userId);
        Task<bool> ProceedToCheckoutAsync(AddToCartDto addToCartDto, Guid userId);
    }
}
