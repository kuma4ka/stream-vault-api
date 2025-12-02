using System.ComponentModel.DataAnnotations.Schema;

namespace StreamVault.Domain.Entities;

public class Broadcast
{
    public Guid Id { get; init; }
    public required string CreatorId { get; init; }
    
    public required string BroadcastTitle { get; set; }
    public required string BroadcastLink { get; init; }
    
    public bool IsLive { get; set; }
    public long ViewCount { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public required User Creator { get; init; }
    public ICollection<BroadcastCategory> BroadcastCategories { get; set; } = new List<BroadcastCategory>();
}