using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
//using ShoppingPortal.Web.Models;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using ShoppingPortal.Services.ProductServices;
using ShoppingPortal.Web.Models;
using ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.Constants.ShoppingPortal.Core.Constants;
using ShoppingPortal.Services.CartServices;
namespace ShoppingPortal.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private const int InitialPageSize = AppConstants.InitialPageSize;
        private const int SubsequentPageSize = AppConstants.SubsequentPageSize;
        private const string ImageBasePath = AppConstants.ImageBasePath;
        private const string DefaultImage = AppConstants.DefaultImage;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            //Assigning Default Images Incase if it doesn't get image
            ViewData["DefaultImage"] = DefaultImage;

            Guid? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            var (products, totalCount) = await _productService.GetPaginatedProductsAsync(1, InitialPageSize,userId);
            var model = new ProductListViewModel
            {
                Products = products.Select(p => new ProductDto
                {
                    // Map other properties
                    Name = p.Name,
                    ProductId = p.ProductId,
                    Description = p.Description,
                    SKU = p.SKU,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                    IsInCart = p.IsInCart,
                    CurrentQuantity = p.CurrentQuantity,
                    ImageUrl = $"{ImageBasePath}{p.ProductId}.webp" // Standardized path
                }), 
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = InitialPageSize,
                    TotalItems = totalCount
                }
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMore(int page)
        {
            Guid? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            var (products, _) = await _productService.GetPaginatedProductsAsync(page, SubsequentPageSize ,userId );
            return PartialView("_ProductCards", products.Select(p => new ProductDto
            {
                // Map other properties
                ImageUrl = $"{ImageBasePath}{p.ProductId}.webp"
            }));
        }


        [ResponseCache(Duration = 3600)] // Cache for 1 hour
        [HttpGet("product/image/{productId}")]
        public IActionResult GetProductImage(Guid productId)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                                       "wwwroot",
                                       ImageBasePath.TrimStart('/'),
                                       $"{productId}.webp");

            if (!System.IO.File.Exists(imagePath))
            {
                return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                                              "wwwroot",
                                              DefaultImage.TrimStart('/')),
                                 "image/webp");
            }

            return PhysicalFile(imagePath, "image/webp");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetCartStatus(Guid productId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = await _cartService.GetCartAsync(userId);
            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            return Json(new
            {
                isInCart = cartItem != null,
                quantity = cartItem?.Quantity ?? 1
            });
        }
    }
}
