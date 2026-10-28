using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Seeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public StoreIdentityDbcontext Dbcontext { get; }

        public IdentityDataSeeder(StoreIdentityDbcontext dbcontext ,
            UserManager<ApplicationUser> userManager ,
            RoleManager<IdentityRole> roleManager)
        {
            Dbcontext = dbcontext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
        var PendingMigrations = await  Dbcontext.Database.GetPendingMigrationsAsync(ct);
            if(PendingMigrations.Any())
                await Dbcontext.Database.MigrateAsync(ct);


            if(!await roleManager.Roles.AnyAsync(ct))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!await userManager.Users.AnyAsync(ct))
            {
                var admin = new ApplicationUser()
                {
                    DisplayName = "Moahmed Nady",
                    Email = "MoahmedNady@gmail.com",
                    UserName = "MoahmedNady",
                    PhoneNumber = "01000000000",

                };
                var CreateUser = await userManager.CreateAsync(admin, "Admin@123");
                if (CreateUser.Succeeded)
                    await userManager.AddToRoleAsync(admin, "SuperAdmin");
                else
                {
                    var errors = string.Join(Environment.NewLine, CreateUser.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create admin user: {errors}");

                }
            }
        }
    }
}
