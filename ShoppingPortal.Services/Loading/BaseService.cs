using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.Loading
{
    public class BaseService
    {

        protected readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        protected BaseService(IMapper mapper,ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        // Eager loading example
        //protected IQueryable<UserDto> GetUserWithEagerLoading(Guid userId)
        //{
        //    return _context.Users
        //        .Include(u => u.Address)
        //        .Include(u => u.Orders)
        //        .Where(u => u.UserId == userId);
        //}

        // Explicit loading example
        //protected async Task<UserDto> GetUserWithExplicitLoadingAsync(Guid userId, bool loadAddress = false, bool loadOrders = false)
        //{
        //    var user = await _context.Users.FindAsync(userId);

        //    if (loadAddress) await _context.LoadReferenceAsync(user, u => u.Address);
        //    if (loadOrders) await _context.LoadCollectionAsync(user, u => u.Orders);

        //    return user;
        //}

        // Lazy loading example (User only)
        //protected User GetUserWithLazyLoading(Guid userId)
        //{
        //    return _context.EnableLazyLoadingForUser(userId);
        //}
    }
}
