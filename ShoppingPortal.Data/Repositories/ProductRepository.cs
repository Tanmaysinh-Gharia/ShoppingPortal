using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedAsync(int page, int pageSize)
        {
            var query = _context.Products
                .Include(p => p.ProductCategories)              // Include join table
                    .ThenInclude(pc => pc.Category)             // Then include Category
                .Where(p => p.StockQuantity > 0)
                .OrderBy(p => p.Name);

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }




        public async Task<Product> GetByIdAsync(Guid productId)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> GetByIdNoTrackingAsync(Guid productId)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }


        // New Get method
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedAsync(
            int page,
            int pageSize,
            string searchTerm = null,
            string sortBy = "name",
            bool sortAsc = true,
            Guid? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            bool? inStock = null)
        {
            var query = _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm) ||
                    p.SKU.Contains(searchTerm));
            }

            // Filter by category
            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.ProductCategories.Any(pc => pc.CategoryId == categoryId.Value));
            }

            // Price range filter
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Stock filter
            if (inStock.HasValue)
            {
                query = inStock.Value
                    ? query.Where(p => p.StockQuantity > 0)
                    : query.Where(p => p.StockQuantity <= 0);
            }

            // Sorting
            query = sortBy.ToLower() switch
            {
                "price" => sortAsc
                    ? query.OrderBy(p => p.Price)
                    : query.OrderByDescending(p => p.Price),
                "name" => sortAsc
                    ? query.OrderBy(p => p.Name)
                    : query.OrderByDescending(p => p.Name),
                "date" => sortAsc
                    ? query.OrderBy(p => p.CreatedAt)
                    : query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderBy(p => p.Name)
            };

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<int> GetProductCountAsync()
        {
            int count = await _context.Products.CountAsync();
            return count;
        }
    }
}
