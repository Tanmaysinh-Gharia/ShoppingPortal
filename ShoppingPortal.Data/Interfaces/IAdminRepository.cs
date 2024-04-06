// ShoppingPortal.Data\Interfaces\IAdminRepository.cs
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.DTOs.Admin;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface IAdminRepository
    {
        // Category Management
        Task<Category> AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid categoryId);
        Task<Category> GetCategoryByIdAsync(Guid categoryId);

        // Product Management
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid productId);
        Task AssignCategoriesToProductAsync(Guid productId, List<Guid> categoryIds);


        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        // Order Management
        Task<PaginatedResult<Order>> GetAllOrdersPaginatedAsync(int page, int pageSize);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusEnum newStatus);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<int> GetCategoryCountAsync();
        Task<List<AdminActivityDto>> GetRecentActivitiesAsync();

    }
}