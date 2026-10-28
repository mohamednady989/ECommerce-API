using ECommerce.Domain.Contracts;
using System.Runtime.CompilerServices;

namespace ECommerce.API.Extencation
{
    public static class WebAppExtension
    {
        public static async Task<WebApplication> SeeedAndMigrateDataAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var Seeder = Scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var IdentitySeeder = Scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");


            await Seeder.SeedDataAsync();
            await IdentitySeeder.SeedDataAsync();
            return app;

        }
    }
}
