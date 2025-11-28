using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users.Delete;
public class DeleteUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("users/{id:guid}", async (
            ISender sender,
            Guid id,
            CancellationToken ct) =>
        {
            _ = await sender.Send(new DeleteUserCommand(id), ct);

            return Results.NoContent();
        })
        .WithName("DeleteUser")
        .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
        .WithSummary("Delete a user by provided identifier")
        .RequireAuthorization("Admin");
    }
}