using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Data.Seeding;
using ECommerce.Infrastructure.Entitys;
<<<<<<< HEAD
using ECommerce.Infrastructure.Payment;
=======
>>>>>>> d495fa98180833c5cf8ecec5b7032f5b4a9133a1
using ECommerce.Infrastructure.Repositres;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Security.Principal;

namespace ECommerce.Infrastructure
{
    public static class InfrastructrureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructrureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbcontext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            // Configure ASP.NET Core Identity to use the Identity DbContext and the custom user
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                // keep defaults, callers can override in Program.cs if needed
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbcontext>();
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");

            services.AddScoped<IUnitOfWork, UnitOFWork>();

            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);

            });
            
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICachRepository, CachRepository>();
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbcontext>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
<<<<<<< HEAD
            services.AddScoped<IPaymentGateway, StripePaymentGateway>();
=======
>>>>>>> d495fa98180833c5cf8ecec5b7032f5b4a9133a1
            


            return services;
        }



    }
}
