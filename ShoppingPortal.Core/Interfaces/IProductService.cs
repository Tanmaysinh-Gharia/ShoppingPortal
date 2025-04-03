using ShoppingPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Interfaces
{
    public interface IProductService
    {
        
            Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetPaginatedProductsAsync(int page, int pageSize, Guid? userId = null);

        Task<ProductDto> GetProductByIdAsync(Guid productId);
    }
}
