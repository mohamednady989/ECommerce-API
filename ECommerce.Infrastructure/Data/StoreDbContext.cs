using ECommerce.Domain.Entites.Order;
using ECommerce.Domain.Entites.Prodects;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Prodects { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
