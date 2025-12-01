using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.Data.Seed;

public static class Initializer
{
    public static async Task SeedData(UserContext context)
    {
        await SeedUsersAsync(context);
    }

    private static async Task SeedUsersAsync(UserContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            await context.Users.AddRangeAsync(InitialData.Users);
            
            await context.SaveChangesAsync();
        }
    }
}