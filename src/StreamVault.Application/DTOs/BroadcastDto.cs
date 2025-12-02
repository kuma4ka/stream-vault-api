namespace StreamVault.Application.DTOs;

public record BroadcastDto(
    Guid Id,
    string Title,
    string Link,
    bool IsLive,
    long ViewCount,
    List<string> Categories,
    DateTime CreatedAt
);