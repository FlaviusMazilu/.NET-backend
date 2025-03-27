using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .IsRequired();
        builder.Property(e => e.Quantity)
            .IsRequired()
            .HasDefaultValue(0);
        builder.Property(e => e.Discount)
            .HasDefaultValue(0);
        builder.HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired();
        builder.HasMany(e => e.Sellers)
            .WithMany(e => e.Products)
            .UsingEntity<ProductSeller>();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}