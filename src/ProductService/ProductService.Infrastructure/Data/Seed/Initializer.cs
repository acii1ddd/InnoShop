using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure.Data.Seed;

public static class Initializer
{
    public static async Task SeedData(ProductContext context)
    {
        await SeedUsersAsync(context);
    }

    private static async Task SeedUsersAsync(ProductContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            
            await context.SaveChangesAsync();
        }
    }
}