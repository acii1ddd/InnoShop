using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;
using UserService.Application.UseCases.Commands.Update;

namespace UserService.API.Endpoints.Users.Put;

public record UpdateUserPasswordRequest(string Password);

public class UpdatePasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}/password", async (
                ISender sender,
                [FromRoute] Guid id,
                [FromBody] UpdateUserPasswordRequest updateUserRoleRequest,
                CancellationToken ct) =>
            {
                var command = new UpdateUserPasswordCommand(id, updateUserRoleRequest.Password);

                _ = await sender.Send(command, ct);

                return Results.NoContent();
            })
            .WithName("UpdateUserPassword")
            .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
            .WithSummary("Update a user password with a provided one")
            .RequireAuthorization("Default");
    }
}