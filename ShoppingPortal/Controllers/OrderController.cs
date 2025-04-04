using Microsoft.AspNetCore.Authorization;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Entities;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Web.Models;
namespace ShoppingPortal.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrderController(
            ICartService cartService,
            IProductService productService,
            IOrderService orderService)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8)
        {
            var userId = GetCurrentUserId();
            var result = await _orderService.GetUserOrdersPaginatedAsync(userId, page, pageSize);

            return View(new OrderListViewModel
            {
                Orders = result.Orders,
                PagingInfo = result.PagingInfo
            });
        }
        // After Order : method
        [HttpGet]
        public async Task<IActionResult> Details(Guid orderId)
        {
            var userId = GetCurrentUserId();
            var order = await _orderService.GetOrderDetailsAsync(userId, orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _orderService.CancelOrderAsync(userId, orderId);

                return Json(new { success });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        // Before Order : method 
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _orderService.PlaceOrderAsync(userId);
                return Json(new { success });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


    }
}
