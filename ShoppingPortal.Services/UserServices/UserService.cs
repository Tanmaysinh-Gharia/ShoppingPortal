using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Services.Loading;
using ShoppingPortal.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.UserServices
{
    public class UserService(ApplicationDbContext context) : BaseService(context),IUserService
    {
        // Example using eager loading
        //public async Task<UserDto> GetUserWithOrdersAsync(Guid userId)
        //{
        //    var user = await GetUserWithEagerLoading(userId)
        //        .Select(u => new UserDto
        //        {
        //            // mapping
        //        })
        //        .FirstOrDefaultAsync();

        //    return user;
        //}

        // Example using explicit loading
        //public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
        //{
        //    var user = await GetUserWithExplicitLoadingAsync(userId, loadAddress: true);
        //    return MapToProfileDto(user);
        //}

        // Example using lazy loading
        //public UserDto GetUserWithLazyLoadedData(Guid userId)
        //{
        //    return GetUserWithLazyLoading(userId);
        //}   
    }
}
