namespace StreamVault.Domain.Entities;

public class BroadcastCategory
{
    public Guid BroadcastId { get; init; }
    public Broadcast Broadcast { get; init; } = null!;

    public Guid CategoryId { get; init; }
    public Category Category { get; init; } = null!;
}