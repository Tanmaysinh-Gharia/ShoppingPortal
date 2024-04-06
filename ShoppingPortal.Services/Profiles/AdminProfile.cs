using AutoMapper;
using ShoppingPortal.Core.DTOs.Admin;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ReverseMap()
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());

            // Order Mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.OrderItemId, opt => opt.MapFrom(src => src.OrderItemId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            // Dashboard Mappings
            CreateMap<Order, AdminActivityDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Order"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    $"New order #{src.OrderId.ToString().Substring(0, 8)} for ${src.TotalAmount}"))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.IconClass, opt => opt.MapFrom(src => "bi-receipt"));

            CreateMap<Product, AdminActivityDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Product"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    $"Product added: {src.Name}"))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.IconClass, opt => opt.MapFrom(src => "bi-box-seam"));
        }
    }
}
