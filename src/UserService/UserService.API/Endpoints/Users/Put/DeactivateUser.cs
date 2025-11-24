using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users.Put;

public class DeactivateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:guid}/deactivate", async (
            ISender sender,
            Guid id,
            CancellationToken ct) =>
        {
            _ = await sender.Send(new DeactivateUserCommand(id), ct);

            return Results.Ok();
        })
        .WithName("DeactivateUser")
        .Produces<StatusCodeResult>()
        .WithSummary("Deactivate user");
    }
}