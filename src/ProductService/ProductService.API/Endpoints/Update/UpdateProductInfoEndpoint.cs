using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.EndpointsSettings;
using ProductService.Application.UseCases.Commands.Update;

namespace ProductService.API.Endpoints.Update;

public record UpdateProductInfoRequest(string Name, string Description, decimal Price);
public class UpdateProductInfoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("product/{productId:guid}", async (
            ISender sender,
            [FromRoute] Guid productId,
            [FromBody] UpdateProductInfoRequest updateProductInfoRequest,
            CancellationToken ct) =>
        {
            var command = new UpdateProductInfoCommand(updateProductInfoRequest.Name,
                updateProductInfoRequest.Description, updateProductInfoRequest.Price, productId);
         
            _ = await sender.Send(command, ct);
            
            return Results.NoContent();
        })
        .WithName("UpdateProductInfo")
        .Produces<StatusCodeResult>(StatusCodes.Status204NoContent)
        .WithSummary("Updates a product name, description, price with a provided information")
        .RequireAuthorization("Default");
    }
}