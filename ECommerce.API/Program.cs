
using ECommerce.API.Extencation;
using ECommerce.Application;
using ECommerce.Application.Commen;
using ECommerce.Application.Profiles;
using ECommerce.Application.Services;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Entitys;

namespace ECommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddInfrastructrureService(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<UrlSetting>(builder.Configuration.GetSection("UrlSettings"));
            builder.Services.Configure<PaymentGatewaySettints>(builder.Configuration.GetSection("Stripe"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();






            //builder.Services.AddOpenApi();

            var app = builder.Build();
            await app.SeeedAndMigrateDataAsync();
            // Configure the HTTP request pipeline.
            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
