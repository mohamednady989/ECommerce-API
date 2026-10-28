using ECommerce.Domain.Entites.Prodects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configrations
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .IsRequired();

            builder.Property(p => p.PictureUrl)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ProductType)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}