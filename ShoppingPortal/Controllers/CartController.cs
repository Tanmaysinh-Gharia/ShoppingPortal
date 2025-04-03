using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingPortal.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var success = await _cartService.AddToCartAsync(addToCartDto, userId);

            if (!success)
            {
                return BadRequest("Failed to add item to cart");
            }

            // In real implementation, you'd get actual cart count
            return Ok(new { cartCount = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> ProceedToCheckout([FromBody] AddToCartDto addToCartDto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var success = await _cartService.ProceedToCheckoutAsync(addToCartDto, userId);

            if (!success)
            {
                return BadRequest("Failed to proceed to checkout");
            }

            return Ok();
        }
    }
}