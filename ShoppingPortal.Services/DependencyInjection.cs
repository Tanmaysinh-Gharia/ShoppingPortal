using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Interfaces;
using ShoppingPortal.Data.Repositories;
using ShoppingPortal.Services.UserServices;

namespace ShoppingPortal.Services
{
    public static class DependencyInjection
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<IUserService, UserService>();
        }

    }
}
