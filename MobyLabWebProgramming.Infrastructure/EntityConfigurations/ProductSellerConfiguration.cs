using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProductSellerConfiguration : IEntityTypeConfiguration<ProductSeller>
{
    public void Configure(EntityTypeBuilder<ProductSeller> builder)
    {
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}