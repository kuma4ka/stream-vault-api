namespace StreamVault.Application.DTOs;

public record CreateBroadcastDto(
    string Title, 
    string Link, 
    List<Guid> CategoryIds
);