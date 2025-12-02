namespace StreamVault.Domain.Entities;

public class Category
{
    public Guid Id { get; init; }
    public required string Name { get; set; }

    public ICollection<BroadcastCategory> BroadcastCategories { get; init; } = new List<BroadcastCategory>();
}