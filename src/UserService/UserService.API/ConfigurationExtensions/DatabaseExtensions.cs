using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Data.Seed;

namespace UserService.API.ConfigurationExtensions;
public static class DatabaseExtensions
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<UserContext>();

        await context.Database.MigrateAsync();

        await Initializer.SeedData(context);
    }
}