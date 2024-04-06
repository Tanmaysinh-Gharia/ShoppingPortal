// ShoppingPortal.Core\Interfaces\IAdminService.cs
using Microsoft.AspNetCore.Http;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.DTOs.Admin;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Interfaces
{
    public interface IAdminService
    {
        // Category Management
        Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto, Guid creatorId);
        Task UpdateCategoryAsync(CategoryDto categoryDto, Guid updatedId);
        Task DeleteCategoryAsync(Guid categoryId);
        Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId);
        Task<List<CategoryDto>> GetAllCategoriesAsync();

        // Product Management
        Task<ProductDto> AddProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto, Guid creatorId);
        Task DeleteProductAsync(Guid productId);
        Task AssignCategoriesToProductAsync(Guid productId, List<Guid> categoryIds);

        // Order Management
        Task<OrderListViewModel> GetAllOrdersPaginatedAsync(int page, int pageSize);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusEnum newStatus);
        Task<OrderDto> GetOrderDetailsAsync(Guid orderId);

        Task<AdminDashboardDto> GetDashboardDataAsync();

        Task<bool> UploadProductImageAsync(Guid productId, IFormFile imageFile);

        Task<ProductDto> AddProductWithCategoriesAsync(ProductDto productDto, List<Guid> categoryIds, IFormFile imageFile, Guid creatorId);

        Task<ProductDto> UpdateProductWithCategoriesAsync(ProductDto productDto, List<Guid> categoryIds, IFormFile imageFile, Guid creatorId);
    }
}