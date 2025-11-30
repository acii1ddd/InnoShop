using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Data.Seed;

namespace ProductService.API.ConfigurationExtensions;

public static class DatabaseExtensions
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ProductContext>();

        await context.Database.MigrateAsync();

        await Initializer.SeedData(context);
    }
}