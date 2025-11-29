using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users.Put;

public class DeactivateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}/deactivate", async (
            ISender sender,
            Guid id,
            CancellationToken ct) =>
        {
            _ = await sender.Send(new DeactivateUserCommand(id), ct);

            return Results.NoContent();
        })
        .WithName("DeactivateUser")
        .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
        .WithSummary("Deactivate user")
        .RequireAuthorization("Admin");
    }
}