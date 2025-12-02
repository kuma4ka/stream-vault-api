using Microsoft.Extensions.DependencyInjection;
using StreamVault.Application.Interfaces.Services;
using StreamVault.Application.Services;

namespace StreamVault.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBroadcastService, BroadcastService>();
        return services;
    }
}