using ECommerce.Infrastructure.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Entitys;

namespace ECommerce.Infrastructure.Data
{
    public class StoreIdentityDbcontext : IdentityDbContext<ApplicationUser>
    {   
        public StoreIdentityDbcontext(DbContextOptions<StoreIdentityDbcontext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<Address>().ToTable("Addresses");



        }
    }
}