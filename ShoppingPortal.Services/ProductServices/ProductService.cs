using AutoMapper;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Interfaces;
using ShoppingPortal.Services.CartServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPortal.Services.UserServices;
using ShoppingPortal.Data.Entities;
using System.Security.Claims;

namespace ShoppingPortal.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        public ProductService(IProductRepository productRepository, IMapper mapper ,ICartService cartService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cartService = cartService;
        }

        public async Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetPaginatedProductsAsync
            (int page, int pageSize, Guid? userId = null)
        {

            var (products, totalCount) = await _productRepository.GetPaginatedAsync(page, pageSize);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();

            if (userId.HasValue)
            {
                var cart = await _cartService.GetCartAsync(userId.Value);

                foreach (var productDto in productDtos)
                {
                    var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productDto.ProductId);
                    if (cartItem != null)
                    {
                        productDto.IsInCart = true;
                        productDto.CurrentQuantity = cartItem.Quantity; // Set actual quantity from DB
                    }
                    else
                    {
                        productDto.IsInCart = false;
                        productDto.CurrentQuantity = 1; // Default if not in cart
                    }
                }
            }

            return (productDtos, totalCount);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }


        public async Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetPaginatedProductsAsync(
            int page,
            int pageSize,
            Guid? userId = null,
            string searchTerm = null,
            string sortBy = "name",
            bool sortAsc = true,
            Guid? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            bool? inStock = null)
        {
            var (products, totalCount) = await _productRepository.GetPaginatedAsync(
                page, pageSize, searchTerm, sortBy, sortAsc, categoryId, minPrice, maxPrice, inStock);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();

            if (userId.HasValue)
            {
                var cart = await _cartService.GetCartAsync(userId.Value);
                foreach (var productDto in productDtos)
                {
                    var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productDto.ProductId);
                    productDto.IsInCart = cartItem != null;
                    productDto.CurrentQuantity = cartItem?.Quantity ?? 1;
                }
            }

            return (productDtos, totalCount);
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _productRepository.GetCategoriesAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

    }
}
