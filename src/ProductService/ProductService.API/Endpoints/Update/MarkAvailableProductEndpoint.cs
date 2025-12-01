using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.EndpointsSettings;
using ProductService.Application.UseCases.Commands.Update;

namespace ProductService.API.Endpoints.Update;

public class MarkAvailableProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{productId:guid}/available", async (
                ISender sender,
                Guid productId,
                CancellationToken ct) =>
            {
                _ = await sender.Send(new MarkAvailableProductCommand(productId), ct);
                
                return Results.NoContent();
            })
            .WithName("MarkAvailableProduct")
            .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
            .WithSummary("Markes product as available")
            .AllowAnonymous();
    }
}