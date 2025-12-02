using StreamVault.Application.DTOs;

namespace StreamVault.Application.Interfaces.Services;

public interface IBroadcastService
{
    Task<BroadcastDto> CreateBroadcastAsync(string creatorId, CreateBroadcastDto dto);
}