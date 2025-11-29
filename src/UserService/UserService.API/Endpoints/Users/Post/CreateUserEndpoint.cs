using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;
using UserService.Application.UseCases.Commands.Create;

namespace UserService.API.Endpoints.Users.Post;

public sealed record CreateUserRequest(string Name, string Email, string Password);

public sealed record CreateUserResponse(Guid UserId);

public class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (
            ISender sender, 
            [FromBody] CreateUserRequest createUserRequest, 
            CancellationToken ct) =>
        {
            var result = await sender.Send(
                new CreateUserCommand(createUserRequest.Name, createUserRequest.Email, createUserRequest.Password), 
                ct
            );

            var response = result.Adapt<CreateUserResponse>();
            
            return Results.Created($"api/users/{response.UserId}", response);
        })
        .WithName("CreateUser")
        .Produces<CreateUserResponse>(StatusCodes.Status201Created)
        .WithSummary("Create a user with provided information")
        .RequireAuthorization("Admin");
    }
}

