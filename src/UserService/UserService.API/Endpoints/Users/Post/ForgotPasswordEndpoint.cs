using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;

namespace UserService.API.Endpoints.Users.Post;

public record ForgotPasswordRequest(string Email);

public record ForgotPasswordResponse(string Message);


public class ForgotPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/forgot-password", async (
            ISender sender, 
            [FromBody] ForgotPasswordRequest forgotPasswordRequest,
            CancellationToken ct) =>
        {
            var command = new ForgotPasswordRequest(forgotPasswordRequest.Email);
            
            _ = await sender.Send(command, ct);

            var result = new ForgotPasswordResponse("Password reset email has been sent.");
            
            return Results.Accepted(value: result);
        })
        .WithName("ForgotPassword")
        .Produces<CreateUserResponse>(StatusCodes.Status202Accepted)
        .WithSummary("Send password reset link to user's email")
        .AllowAnonymous();
    }
}