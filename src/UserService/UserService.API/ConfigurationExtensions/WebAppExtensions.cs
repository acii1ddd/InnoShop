using Scalar.AspNetCore;
using UserService.API.EndpointsSettings;

namespace UserService.API.ConfigurationExtensions;

public static class WebAppExtensions
{
    public static async Task ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        // map endpoints
        var mapGroup = app.MapGroup("/api/users");
        app.MapEndpoints(mapGroup);
        
        await app.InitDbAsync();
    }
}