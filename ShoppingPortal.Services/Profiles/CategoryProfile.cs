using AutoMapper;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());
        }
    }
}
