using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands.Update;

namespace UserService.API.Endpoints.Users.Put;

public record UpdateUserRequest(string Name, string Email);
public class UpdateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}", async (
                ISender sender,
                [FromRoute] Guid id,
                [FromBody] UpdateUserRequest updateUserRequest,
                CancellationToken ct) =>
            {
                var command = new UpdateUserCommand(id, updateUserRequest.Name,
                    updateUserRequest.Email);

                _ = await sender.Send(command, ct);

                return Results.NoContent();
            })
            .WithName("UpdateUser")
            .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
            .WithSummary("Update a user name, email with provided information")
            .RequireAuthorization("Default");
    }
}