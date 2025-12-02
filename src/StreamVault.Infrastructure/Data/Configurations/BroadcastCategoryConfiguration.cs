using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamVault.Domain.Entities;

namespace StreamVault.Infrastructure.Data.Configurations;

public class BroadcastCategoryConfiguration : IEntityTypeConfiguration<BroadcastCategory>
{
    public void Configure(EntityTypeBuilder<BroadcastCategory> builder)
    {
        builder.HasKey(bc => new { bc.BroadcastId, bc.CategoryId });

        builder.HasOne(bc => bc.Broadcast)
            .WithMany(b => b.BroadcastCategories)
            .HasForeignKey(bc => bc.BroadcastId);

        builder.HasOne(bc => bc.Category)
            .WithMany(c => c.BroadcastCategories)
            .HasForeignKey(bc => bc.CategoryId);
    }
}