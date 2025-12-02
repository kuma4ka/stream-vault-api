using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StreamVault.Domain.Entities;

namespace StreamVault.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        try 
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
                Log.Information("Applied pending database migrations.");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while applying migrations.");
        }

        var testUserEmail = "streamer@test.com";
        
        if (await userManager.FindByEmailAsync(testUserEmail) == null)
        {
            var user = new User
            {
                Id = "test-streamer-id",
                UserName = "streamer",
                Email = testUserEmail,
                EmailConfirmed = true
            };

            var password = configuration["Seeding:DefaultPassword"] 
                           ?? throw new Exception("Seeding:DefaultPassword is not configured in User Secrets.");

            var result = await userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                Log.Information("Test user created successfully.");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Log.Error($"Failed to create test user: {errors}");
            }
        }
    }
}