using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;
using UserService.Application.UseCases.Commands.Update;

namespace UserService.API.Endpoints.Users.Put;

public record UpdateUserRoleRequest(string Role);

public class UpdateUserRoleEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}/role", async (
                ISender sender,
                [FromRoute] Guid id,
                [FromBody] UpdateUserRoleRequest updateUserRoleRequest,
                CancellationToken ct) =>
            {
                var command = new UpdateUserRoleCommand(id, updateUserRoleRequest.Role);

                _ = await sender.Send(command, ct);

                return Results.NoContent();
            })
            .WithName("UpdateUserRole")
            .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
            .WithSummary("Update a user role with provided role")
            .RequireAuthorization("Admin");
    }
}