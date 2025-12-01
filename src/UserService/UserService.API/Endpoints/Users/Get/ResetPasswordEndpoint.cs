using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands.Recovery;

namespace UserService.API.Endpoints.Users.Get;

public sealed record ResetPasswordResponse(string Message);

public class ResetPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reset-password", async (
            [FromQuery] string token,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new ResetPasswordCommand(token), ct);

            var response = result.Adapt<ResetPasswordResponse>();
            
            return Results.Ok(response);
        })
        .WithName("ResetPassword")
        .Produces<StatusCodeResult>()
        .WithSummary("Resets user password")
        .AllowAnonymous();
    }
}