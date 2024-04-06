// ShoppingPortal.Services\AdminServices\AdminService.cs
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.Constants.ShoppingPortal.Core.Constants;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.DTOs.Admin;
using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Core.Models;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Http;

using static System.Net.Mime.MediaTypeNames;

namespace ShoppingPortal.Services.AdminServices
{
    public class AdminService : IAdminService
    {

        private readonly IAdminRepository _adminRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        private const string ImageBasePath = AppConstants.ImageBasePath;

        public AdminService(IAdminRepository adminRepository, IMapper mapper, IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _adminRepository = adminRepository;

            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<AdminDashboardDto> GetDashboardDataAsync()
        {
            return new AdminDashboardDto
            {
                CategoryCount = await _adminRepository.GetCategoryCountAsync(),
                ProductCount = await _productRepository.GetProductCountAsync(),
                OrderCount = await _orderRepository.GetOrderCountAsync(),
                RecentActivities = await _adminRepository.GetRecentActivitiesAsync()
            };
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto,Guid creatorId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.CreatedBy = creatorId;
            var result = await _adminRepository.AddCategoryAsync(category);
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto, Guid updatedId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.CreatedBy = updatedId;
            await _adminRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            await _adminRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _adminRepository.GetCategoryByIdAsync(categoryId);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _adminRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var result = await _adminRepository.AddProductAsync(product);
            return _mapper.Map<ProductDto>(result);
        }

        public async Task UpdateProductAsync(ProductDto productDto, Guid creatorId)
        {
            var product = _mapper.Map<Product>(productDto);
            product.CreatedBy = creatorId;
            await _adminRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await _adminRepository.DeleteProductAsync(productId);
        }

        public async Task AssignCategoriesToProductAsync(Guid productId, List<Guid> categoryIds)
        {
            await _adminRepository.AssignCategoriesToProductAsync(productId, categoryIds);
        }

        public async Task<OrderListViewModel> GetAllOrdersPaginatedAsync(int page, int pageSize)
        {
            var result = await _adminRepository.GetAllOrdersPaginatedAsync(page, pageSize);

            var orderDtos = result.Items.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems?.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown Product",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Status = oi.Status
                }).ToList() ?? new List<OrderItemDto>()
            }).ToList();

            return new OrderListViewModel
            {
                Orders = orderDtos,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = result.PageNumber,
                    ItemsPerPage = result.PageSize,
                    TotalItems = result.TotalCount
                }
            };
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusEnum newStatus)
        {
            return await _adminRepository.UpdateOrderStatusAsync(orderId, newStatus);
        }

        public async Task<OrderDto> GetOrderDetailsAsync(Guid orderId)
        {
            // Implement this method to fetch order details
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null) return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }

        public async Task<bool> UploadProductImageAsync(Guid productId, IFormFile imageFile)
        {
            try
            {
                // Validate the image file
                if (imageFile == null || imageFile.Length == 0)
                {
                    return false;
                }

                if (imageFile.Length > 5 * 1024 * 1024) // 5MB
                {
                    return false;
                }
                // Validate file extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return false;
                }

                // Create the uploads directory if it doesn't exist
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImageBasePath.TrimStart('/'));
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                // Generate the filename (using productId for consistency)
                var fileName = $"{productId}.webp";
                var filePath = Path.Combine(uploadsDirectory, fileName);

                // Delete existing image if it exists
                var existingFiles = Directory.GetFiles(uploadsDirectory, $"{productId}.*");
                foreach (var existingFile in existingFiles)
                {
                    System.IO.File.Delete(existingFile);
                }

                // Save the new image
                using (var imageStream = imageFile.OpenReadStream())
                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(imageStream))
                {
                    // Optional: Resize image if needed
                    // image.Mutate(x => x.Resize(new ResizeOptions
                    // {
                    //     Size = new Size(800, 800),
                    //     Mode = ResizeMode.Max
                    // }));

                    await image.SaveAsync(filePath, new WebpEncoder
                    {
                        Quality = 80, // Adjust quality (1-100)
                        Method = (WebpEncodingMethod) WebpFileFormatType.Lossy // or Lossless for better quality
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the error
                return false;
            }
        }

        public async Task<ProductDto> AddProductWithCategoriesAsync(ProductDto productDto, List<Guid> categoryIds, IFormFile imageFile, Guid creatorId)
        {
            try
            {
                // Begin transaction at service level
                await _adminRepository.BeginTransactionAsync();

                // 1. Add Product
                var product = _mapper.Map<Product>(productDto);

                Product existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
                product.CreatedBy = creatorId;
                product.RowVersion = existingProduct.RowVersion;
                var addedProduct = await _adminRepository.AddProductAsync(product);

                // 2. Upload Image (if provided)
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadSuccess = await UploadProductImageAsync(addedProduct.ProductId, imageFile);
                    if (!uploadSuccess)
                    {
                        throw new Exception("Failed to upload product image");
                    }
                }

                // 3. Assign Categories
                if (categoryIds != null && categoryIds.Any())
                {
                    await _adminRepository.AssignCategoriesToProductAsync(addedProduct.ProductId, categoryIds);
                }

                // Commit transaction if all operations succeed
                await _adminRepository.CommitTransactionAsync();

                return _mapper.Map<ProductDto>(addedProduct);
            }
            catch (Exception)
            {
                await _adminRepository.RollbackTransactionAsync();
                throw; // Re-throw to be handled by controller
            }
        }


        public async Task<ProductDto> UpdateProductWithCategoriesAsync(ProductDto productDto, List<Guid> categoryIds, IFormFile imageFile, Guid creatorId)
        {
            await _adminRepository.BeginTransactionAsync();

            try
            {
                // 1. Get current product with tracking
                var existingProduct = await _productRepository.GetByIdNoTrackingAsync(productDto.ProductId);

                if (existingProduct == null)
                {
                    throw new Exception("Product not found");
                }

                // 2. Update product
                var product = _mapper.Map<Product>(productDto);
                product.RowVersion = existingProduct.RowVersion; // Preserve concurrency token
                product.CreatedBy = creatorId;
                await _adminRepository.UpdateProductAsync(product);

                // 3. Handle image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    await UploadProductImageAsync(product.ProductId, imageFile);
                }

                // 4. Update categories
                await _adminRepository.AssignCategoriesToProductAsync(product.ProductId, categoryIds);

                // 5. Commit transaction
                await _adminRepository.CommitTransactionAsync();

                return productDto;
            }
            catch (Exception)
            {
                await _adminRepository.RollbackTransactionAsync();
                throw;
            }
        }
    }
}