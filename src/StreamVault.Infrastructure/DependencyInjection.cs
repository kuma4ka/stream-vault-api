using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamVault.Application.Interfaces.Repositories; // Додано
using StreamVault.Domain.Entities;
using StreamVault.Infrastructure.Data;
using StreamVault.Infrastructure.Repositories;

namespace StreamVault.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IBroadcastRepository, BroadcastRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}