using ECommerce.Domain.Entites.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Configrations
{
    public  class OrderConfigration:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(O => O.Items)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(X => X.subtotal).HasColumnType("decimal(8,2)");
            builder.OwnsOne(O => O.ShippingAdderss, address =>
            {
                address.Property(P => P.FirstName).HasMaxLength(50);
                address.Property(P => P.LastName).HasMaxLength(50);
                address.Property(P => P.City).HasMaxLength(50);
                address.Property(P => P.Street).HasMaxLength(50);
                address.Property(P => P.Country).HasMaxLength(50);
                


            });
            builder.Property(X => X.Status).HasConversion<string>().HasMaxLength(50);


        }

        
    }
}
