using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamVault.Domain.Entities;

namespace StreamVault.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.HasData(
            new Category { Id = Guid.Parse("1534b87c-3947-4e12-b21b-5cca2e484b72"), Name = "Action" },
            new Category { Id = Guid.Parse("12b06521-4f86-481c-b9b4-c0c9e4dafa8a"), Name = "Adventure" },
            new Category { Id = Guid.Parse("e385b27f-248b-4640-b084-aa6311c93f26"), Name = "RPG" },
            new Category { Id = Guid.Parse("6d8ebff6-9a78-45d7-8db3-f33f131a6d3b"), Name = "Shooter" },
            new Category { Id = Guid.Parse("3e04cc74-d8c3-46f2-9277-f3178bd48b9c"), Name = "Strategy" }
        );
    }
}