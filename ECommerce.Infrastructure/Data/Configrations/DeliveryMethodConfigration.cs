using ECommerce.Domain.Entites.Order;
using ECommerce.Domain.Entites.Prodects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Configrations
{
    public class DeliveryMethodConfigration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(X => X.Price).HasColumnType("decimal(8,2)");
            builder.Property(X => X.ShortName).HasMaxLength(60);
            builder.Property(X => X.Description).HasMaxLength(60);
            builder.Property(X => X.DeliveryTime).HasColumnType("varchar").HasMaxLength(100);

        }
    }
}
