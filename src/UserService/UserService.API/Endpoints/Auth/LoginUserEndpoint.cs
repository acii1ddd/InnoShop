using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.Dtos;
using UserService.Application.UseCases.Commands.Auth;

namespace UserService.API.Endpoints.Auth;
public sealed record LoginUserRequest(string Email, string Password);
public sealed record LoginUserResponse(LoginUserDto User);
public class LoginUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async (
            ISender sender,
            [FromBody] LoginUserRequest loginUserRequest,
            CancellationToken ct) =>
        {
            var result = await sender.Send(
                new LoginUserCommand(loginUserRequest.Email, loginUserRequest.Password), 
            ct);

            var response = result.Adapt<LoginUserResponse>();
            return Results.Ok(response);
        })
        .WithName("Login")
        .Produces<StatusCodeResult>()
        .WithSummary("Login a user by provided credentials")
        .AllowAnonymous();
    }
}