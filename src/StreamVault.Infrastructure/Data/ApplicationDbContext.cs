using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreamVault.Domain.Entities;
using System.Reflection;

namespace StreamVault.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<User>(options)
{
    public DbSet<Broadcast> Broadcasts { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<BroadcastCategory> BroadcastCategories { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}