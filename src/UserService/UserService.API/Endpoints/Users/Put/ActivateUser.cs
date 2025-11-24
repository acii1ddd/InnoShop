using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users.Put;

public class ActivateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:guid}/activate", async (
            ISender sender,
            Guid id,
            CancellationToken ct) =>
        {
            _ = await sender.Send(new ActivateUserCommand(id), ct);

            return Results.NoContent();
        })
        .WithName("ActivateUser")
        .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
        .WithSummary("Activate user");
    }
}