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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginRole>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserType))
                .ForMember(dest => dest.Username , opt => opt.MapFrom(src => src.Firstname + " " + src.Lastname));
        }

    }
}
