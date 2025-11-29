using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Commands;

namespace UserService.API.Endpoints.Users.Put;

public sealed record ConfirmEmailResponse(string Message);
public class ConfirmEmailEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/confirm-email", async (
            [FromQuery] string token, 
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new ConfirmEmailCommand(token), ct);

            var response = result.Adapt<ConfirmEmailResponse>();
            
            return Results.Ok(response);
        })
        .WithName("ConfirmEmail")
        .Produces<StatusCodeResult>()
        .WithSummary("Confirm your email")
        .AllowAnonymous();
    }
}