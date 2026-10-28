using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entites.Order;
using ECommerce.Domain.Entites.Prodects;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ECommerce.Infrastructure.Data.Seeding
{
    public class CatalogDataSeeder(StoreDbContext dbContext) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigration.Any())
                    await dbContext.Database.MigrateAsync(ct);
                var seedRoot = Path.Combine(AppContext.BaseDirectory, "Dataseed");
                await SeedIfEmptyAsync<ProductBrand, int>(seedRoot, "brands.json", ct);
                await SeedIfEmptyAsync<ProductType, int>(seedRoot, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(seedRoot, "products.json", ct);
                await SeedIfEmptyAsync<DeliveryMethod, int>(seedRoot, "delivery.json", ct);


                int result = await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                    Console.WriteLine($"Data Seeded Success");
                else
                    Console.WriteLine("Data seed Failald ");




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        private async Task SeedIfEmptyAsync<T, TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            {
                Console.WriteLine("Seeding Failed... Data Exists in the Table ");
                return;
            }
            var FilePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(FilePath))
                return;

            using var fileStream = File.OpenRead(FilePath);

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream);
            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);




        }
    }
}
