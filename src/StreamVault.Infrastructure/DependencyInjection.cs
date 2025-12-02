using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamVault.Application.Interfaces.Repositories;
using StreamVault.Application.Interfaces.Data;
using StreamVault.Application.Interfaces.Services;
using StreamVault.Infrastructure.Auth;
using StreamVault.Domain.Entities;
using StreamVault.Infrastructure.Data;
using StreamVault.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

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
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IBroadcastRepository, BroadcastRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        services.AddScoped<JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}