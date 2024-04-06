using AutoMapper;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Core.Models;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Core.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.Profiles
{
    public class AdminViewModelProfile : Profile
    {
        //public AdminViewModelProfile()
        //{
        //    // Category View Models
        //    CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();

        //    // Product View Models
        //    CreateMap<ProductDto, ProductViewModel>()
        //        .ForMember(dest => dest.SelectedCategoryIds,
        //            opt => opt.MapFrom(src => src.Categories.Select(c => c.CategoryId)))
        //        .ReverseMap();

        //    // Order View Models
        //    CreateMap<OrderDto, OrderViewModel>().ReverseMap();
        //    CreateMap<OrderItemDto, OrderItemViewModel>().ReverseMap();
        //    CreateMap<PaginatedResult<Order>, OrderListViewModel>()
        //        .ForMember(dest => dest.Orders,
        //            opt => opt.MapFrom(src => src.Items))
        //        .ForMember(dest => dest.PagingInfo,
        //            opt => opt.MapFrom(src => new PagingInfo
        //            {
        //                CurrentPage = src.PageNumber,
        //                ItemsPerPage = src.PageSize,
        //                TotalItems = src.TotalCount
        //            }));
        //}
    }
}
