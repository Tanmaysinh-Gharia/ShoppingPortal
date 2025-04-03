using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(
            ICartRepository cartRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> AddToCartAsync(AddToCartDto addToCartDto, Guid userId)
        {
            // Implementation to add item to cart
            return true;
        }

        public async Task<bool> ProceedToCheckoutAsync(AddToCartDto addToCartDto, Guid userId)
        {
            // Implementation to proceed to checkout
            return true;
        }
    }
}
