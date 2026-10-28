using ECommerce.Domain.Entites.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Configrations
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(X => X.Price).HasColumnType("decimal(8,2)");
            builder.OwnsOne(X => X.Product, product =>
            {
                product.Property(X => X.ProductName).HasMaxLength(100);
                product.Property(X => X.PictureUrl).HasMaxLength(100);



            });
        }
    }
}
