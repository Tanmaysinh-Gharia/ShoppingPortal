using ShoppingPortal.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Services.UserServices
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _context;

        protected BaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Eager loading example
        //protected IQueryable<User> GetUserWithEagerLoading(Guid userId)
        //{
        //    return _context.Users
        //        .Include(u => u.Address)
        //        .Include(u => u.Orders)
        //        .Where(u => u.UserId == userId);
        //}

        // Explicit loading example
        //protected async Task<User> GetUserWithExplicitLoadingAsync(Guid userId, bool loadAddress = false, bool loadOrders = false)
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
