using StreamVault.Domain.Entities;

namespace StreamVault.Application.Interfaces.Repositories;

public interface IBroadcastRepository
{
    Task AddAsync(Broadcast broadcast);
    Task<Broadcast?> GetByIdAsync(Guid id);
}   