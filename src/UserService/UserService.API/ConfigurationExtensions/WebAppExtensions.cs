namespace UserService.API.ConfigurationExtensions;

public static class WebAppExtensions
{
    public static async Task ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseAuthorization();
        await app.InitDbAsync();
    }
}