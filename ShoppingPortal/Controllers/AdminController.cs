// ShoppingPortal.Web\Controllers\AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.Constants.ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Interfaces;
//using ShoppingPortal.Core.Models;
using ShoppingPortal.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingPortal.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IProductService _productService;
        private const int InitialPageSize = AppConstants.InitialPageSize;
        private const int SubsequentPageSize = AppConstants.SubsequentPageSize;
        private const string ImageBasePath = AppConstants.ImageBasePath;
        private const string DefaultImage = AppConstants.DefaultImage;

        public AdminController(IAdminService adminService, IProductService productService)
        {
            _adminService = adminService;
            _productService = productService;
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public async Task<IActionResult> Index()
        {
            var dashboardData = await _adminService.GetDashboardDataAsync();
            return View(dashboardData);
        }

        #region Category Management

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _adminService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            CategoryDto categoryDto = new CategoryDto();
            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }
            Guid creatorId = GetCurrentUserId();
            await _adminService.AddCategoryAsync(categoryDto, creatorId);
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid id)
        {
            var category = await _adminService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost("/Admin/EditCategory/{id}")]
        public async Task<IActionResult> EditCategory(Guid id, CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }
            Guid updatedId = GetCurrentUserId();
            categoryDto.CategoryId = id;
            await _adminService.UpdateCategoryAsync(categoryDto, updatedId);
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _adminService.DeleteCategoryAsync(id);
            return RedirectToAction("Categories");
        }

        #endregion

        #region Product Management

        [HttpGet]
        public async Task<IActionResult> Products(
            string searchTerm = null,
            string sortBy = "name",
            bool sortAsc = true,
            Guid? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            bool? inStock = null)
        {
            //Assigning Default Images Incase if it doesn't get image
            ViewData["DefaultImage"] = DefaultImage;

            Guid? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            var (products, totalCount) = await _productService.GetPaginatedProductsAsync(
                    1, InitialPageSize, userId, searchTerm, sortBy, sortAsc, categoryId, minPrice, maxPrice, inStock);

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
                    Categories = p.Categories,
                    IsInCart = p.IsInCart,
                    CurrentQuantity = p.CurrentQuantity,
                    ImageUrl = $"{ImageBasePath}{p.ProductId}.webp" // Standardized path
                }),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = InitialPageSize,
                    TotalItems = totalCount
                },
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortAsc = sortAsc,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                InStock = inStock
            };

            ViewBag.Categories = await _productService.GetCategoriesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
            return View(new ProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto, List<Guid> categoryIds, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
                return View(productDto);
            }

            try
            {
                productDto.ProductId = Guid.NewGuid();
                Guid creatorId = GetCurrentUserId();
                await _adminService.AddProductWithCategoriesAsync(productDto, categoryIds, imageFile,creatorId);
                return RedirectToAction("Products","Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding the product: " + ex.Message);
                ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
                return View(productDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDto productDto, List<Guid> categoryIds, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
                return View(productDto);
            }

            try
            {

                Guid creatorId = GetCurrentUserId();
                await _adminService.UpdateProductWithCategoriesAsync(productDto, categoryIds, imageFile,creatorId);
                return RedirectToAction("Products");
            }
            catch (Exception ex) when (ex.InnerException is DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "This record was modified by another user. Please refresh and try again.");
                ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
                return View(productDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Categories = await _adminService.GetAllCategoriesAsync();
                return View(productDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _adminService.DeleteProductAsync(id);
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Products");
            }
        }
        #endregion

        #region Order Management

        [HttpGet]
        public async Task<IActionResult> Orders(int page = 1, int pageSize = 10)
        {
            var orders = await _adminService.GetAllOrdersPaginatedAsync(page, pageSize);
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, OrderStatusEnum newStatus)
        {
            var success = await _adminService.UpdateOrderStatusAsync(orderId, newStatus);
            return Json(new { success });
        }


        [HttpGet]
        public async Task<IActionResult> OrderDetailsPartial(Guid id)
        {
            var order = await _adminService.GetOrderDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return PartialView("_OrderDetailsPartial", order);
        }
        #endregion
    }
}