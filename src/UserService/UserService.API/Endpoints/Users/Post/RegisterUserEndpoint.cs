using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands.Auth;

namespace UserService.API.Endpoints.Users.Post;

public sealed record RegisterUserRequest(string Name, string Email, string Password);

public sealed record RegisterUserResponse(Guid UserId);

public class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("register", async (
            ISender sender, 
            [FromBody]  CreateUserRequest request,
            CancellationToken ct) =>
        {
            var result = await sender.Send(
                new RegisterUserCommand(request.Name, request.Email, request.Password), 
                ct
            );
            
            var response = result.Adapt<RegisterUserResponse>();
            
            return Results.Ok(response);
        })
        .WithName("RegisterUser")
        .Produces<StatusCodeResult>()
        .WithSummary("Register user")
        .AllowAnonymous();
    }
}