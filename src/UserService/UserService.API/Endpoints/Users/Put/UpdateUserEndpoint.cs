using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users.Put;

public record UpdateUserRequest(string Name, string Email, string Role);
public class UpdateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:guid}", async (
            ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateUserRequest updateUserRequest,
            CancellationToken ct) =>
        {
            var command = new UpdateUserCommand(id, updateUserRequest.Name, 
                updateUserRequest.Email, updateUserRequest.Role);

            _ = await sender.Send(command, ct);

            return Results.Ok();
        })
        .WithName("UpdateUser")
        .Produces<StatusCodeResult>()
        .WithSummary("Update a user name, email, role with provided information");
    }
}