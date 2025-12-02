using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamVault.Domain.Entities;

namespace StreamVault.Infrastructure.Data.Configurations;

public class BroadcastConfiguration : IEntityTypeConfiguration<Broadcast>
{
    public void Configure(EntityTypeBuilder<Broadcast> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BroadcastTitle)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.BroadcastLink)
            .IsRequired();

        builder.HasOne(b => b.Creator)
            .WithMany(u => u.Broadcasts)
            .HasForeignKey(b => b.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}