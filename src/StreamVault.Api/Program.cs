using StreamVault.Application;
using StreamVault.Infrastructure;
using StreamVault.Infrastructure.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            await DbInitializer.SeedAsync(scope.ServiceProvider);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed during database seeding.");
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();