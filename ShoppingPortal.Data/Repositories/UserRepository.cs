using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Entities;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
namespace ShoppingPortal.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        
    }
}
