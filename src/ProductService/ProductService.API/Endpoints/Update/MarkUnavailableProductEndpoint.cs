using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.EndpointsSettings;
using ProductService.Application.UseCases.Commands.Update;

namespace ProductService.API.Endpoints.Update;

public class MarkUnavailableProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{productId:guid}/unavailable", async (
                ISender sender,
                Guid productId,
                CancellationToken ct) =>
            {
                _ = await sender.Send(new MarkUnavailableProductCommand(productId), ct);
                
                return Results.NoContent();
            })
            .WithName("MarkUnavailableProduct")
            .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
            .WithSummary("Markes product as unavailable")
            .AllowAnonymous();
    }
}