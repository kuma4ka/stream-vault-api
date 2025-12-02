using StreamVault.Domain.Entities;

namespace StreamVault.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetByIdsAsync(List<Guid> ids);
}