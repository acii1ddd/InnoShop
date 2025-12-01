using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.EndpointsSettings;
using ProductService.Application.UseCases.Commands.Delete;

namespace ProductService.API.Endpoints.Delete;

public class DeleteProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("users/{productId:guid}", async (
                ISender sender,
                Guid productId,
                CancellationToken ct) =>
            {
                _ = await sender.Send(new DeleteProductCommand(productId), ct);

                return Results.NoContent();
            })
            .WithName("DeleteProduct")
            .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
            .WithSummary("Deletes a product by provided identifier")
            .RequireAuthorization("Default");
    }
}