using AutoMapper;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetPaginatedProductsAsync(int page, int pageSize)
        {
            var (products, totalCount) = await _productRepository.GetPaginatedAsync(page, pageSize);
            return (_mapper.Map<IEnumerable<ProductDto>>(products), totalCount);
        }
    }
}
