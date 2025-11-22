using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users;
public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (CreateHandler createHandler) =>
        {
            var result = await createHandler.HandleAsync();
            
            return result;
        });
    }
}

