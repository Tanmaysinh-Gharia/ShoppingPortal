using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedAsync(int page, int pageSize);

        Task<Product> GetByIdAsync(Guid productId);

        Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedAsync(
                int page,
                int pageSize,
                string searchTerm = null,
                string sortBy = "name",
                bool sortAsc = true,
                Guid? categoryId = null,
                decimal? minPrice = null,
                decimal? maxPrice = null,
                bool? inStock = null);

        Task<List<Category>> GetCategoriesAsync();

        Task<int> GetProductCountAsync();

        Task<Product> GetByIdNoTrackingAsync(Guid productId);
    }
}
