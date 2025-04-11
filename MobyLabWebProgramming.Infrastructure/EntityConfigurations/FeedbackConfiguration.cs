using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.MostBoughtProductCategory).IsRequired();
        builder.Property(e => e.Functionalities).IsRequired();
        builder.Property(e => e.GeneralExperience).IsRequired();
    }
}