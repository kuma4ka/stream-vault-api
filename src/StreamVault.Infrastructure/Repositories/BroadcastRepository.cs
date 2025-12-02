using Microsoft.EntityFrameworkCore;
using StreamVault.Application.Interfaces.Repositories;
using StreamVault.Domain.Entities;
using StreamVault.Infrastructure.Data;

namespace StreamVault.Infrastructure.Repositories;

public class BroadcastRepository(ApplicationDbContext context) : IBroadcastRepository
{
    public async Task AddAsync(Broadcast broadcast)
    {
        await context.Broadcasts.AddAsync(broadcast);
        await context.SaveChangesAsync();
    }

    public async Task<Broadcast?> GetByIdAsync(Guid id)
    {
        return await context.Broadcasts
            .Include(b => b.BroadcastCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}