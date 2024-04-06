using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Entities;
namespace ShoppingPortal.Services.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product to ProductDto mapping
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.ProductCategories.Select(pc => new CategoryDto
                    {
                        CategoryId = pc.Category.CategoryId,
                        Name = pc.Category.Name
                    }).ToList()))
                .ForMember(dest => dest.CurrentQuantity, opt => opt.Ignore())
                .ForMember(dest => dest.IsInCart, opt => opt.Ignore());

            // Category to CategoryDto mapping
            CreateMap<Category, CategoryDto>();


            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());
        }
    }
}
