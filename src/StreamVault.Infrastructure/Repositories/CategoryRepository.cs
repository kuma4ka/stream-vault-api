using Microsoft.EntityFrameworkCore;
using StreamVault.Application.Interfaces.Repositories;
using StreamVault.Domain.Entities;
using StreamVault.Infrastructure.Data;

namespace StreamVault.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetByIdsAsync(List<Guid> ids)
    {
        return await context.Categories
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
    }
}