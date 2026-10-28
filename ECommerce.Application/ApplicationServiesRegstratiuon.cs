using ECommerce.Application.Contracts;
using ECommerce.Application.Services;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application
{
    public static   class ApplicationServiesRegstratiuon
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(C => { }, typeof(ApplicationServiesRegstratiuon).Assembly);
            services.AddScoped<IproductService, Services.ProductService>();
            services.AddScoped<IBasketService, Services.BasketService>();
            services.AddScoped<ICacheService, Services.CacheService>();
            services.AddScoped<IAuthenticationService,AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService,PaymentService >();





            return services;
        }
    }
}
