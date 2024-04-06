// ShoppingPortal.Data\Repositories\AdminRepository.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShoppingPortal.Core.DTOs.Admin;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _currentTransaction;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Detach any existing tracked entity with same ID
            var existingEntity = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(e => e.Entity.ProductId == product.ProductId);
            if (existingEntity != null)
            {
                existingEntity.State = EntityState.Detached;
            }

            // Attach and update
            _context.Products.Update(product);

            // Explicitly mark RowVersion as modified to prevent concurrency issues
            _context.Entry(product).Property(x => x.RowVersion).IsModified = true;

            // Don't call SaveChangesAsync here - it will be handled by the transaction
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignCategoriesToProductAsync(Guid productId, List<Guid> categoryIds)
        {
            // Remove existing associations without loading them
            await _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .ExecuteDeleteAsync();

            // Add new associations
            var newAssociations = categoryIds.Select(categoryId => new ProductCategory
            {
                ProductId = productId,
                CategoryId = categoryId
            });

            await _context.ProductCategories.AddRangeAsync(newAssociations);
            // Don't call SaveChangesAsync here - it will be handled by the transaction
        }

        public async Task<PaginatedResult<Order>> GetAllOrdersPaginatedAsync(int page, int pageSize)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .OrderByDescending(o => o.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Order>(items, totalCount, page, pageSize);
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusEnum newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            if (order.Status == OrderStatusEnum.Cancelled) return false;
            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }


        public async Task<int> GetCategoryCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<List<AdminActivityDto>> GetRecentActivitiesAsync()
        {
            // Get last 5 activities from different tables
            var activities = new List<AdminActivityDto>();

            // Recent orders
            var recentOrders = await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .Take(3)
                .Select(o => new AdminActivityDto
                {
                    Type = "Order",
                    Description = $"New order #{o.OrderId.ToString().Substring(0, 8)} for ${o.TotalAmount}",
                    Timestamp = o.CreatedAt,
                    IconClass = "bi-receipt"
                })
                .ToListAsync();

            activities.AddRange(recentOrders);

            // Recent products
            var recentProducts = await _context.Products
                .OrderByDescending(p => p.CreatedAt)
                .Take(2)
                .Select(p => new AdminActivityDto
                {
                    Type = "Product",
                    Description = $"Product added: {p.Name}",
                    Timestamp = p.CreatedAt,
                    IconClass = "bi-box-seam"
                })
                .ToListAsync();

            activities.AddRange(recentProducts);

            return activities.OrderByDescending(a => a.Timestamp).Take(5).ToList();
        }
    

    public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress");
            }
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                        await _currentTransaction.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        await _currentTransaction.RollbackAsync();
                        // Refresh the RowVersion for the next attempt
                        foreach (var entry in ex.Entries)
                        {
                            var databaseValues = await entry.GetDatabaseValuesAsync();
                            if (databaseValues != null)
                            {
                                entry.OriginalValues.SetValues(databaseValues);
                            }
                        }
                        throw new Exception("Concurrency conflict occurred. Please try again.");
                    }
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
    }
}