using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.EndpointsSettings;
using ProductService.Application.Dtos;
using ProductService.Application.UseCases.Queries.GetAll;

namespace ProductService.API.Endpoints.Get.GetAll;

public sealed record GetProductsResponse(IReadOnlyList<ProductWithUserDto> Products);

public class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
                ISender sender,
                [FromQuery] Guid? userId,
                [FromQuery] string? name,
                [FromQuery] decimal? minPrice,
                [FromQuery] decimal? maxPrice,
                [FromQuery] DateTime? createdAfter,
                [FromQuery] DateTime? createdBefore,
                CancellationToken ct) =>
            {
                var query = new GetProductsQuery(
                    userId,
                    name,
                    minPrice,
                    maxPrice,
                    createdAfter,
                    createdBefore
                );
                
                var result = await sender.Send(query, ct);

                var response = result.Adapt<GetProductsResponse>();
                
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .WithSummary("Get a list of all available products with optional filters")
            .RequireAuthorization("Default");
    }
}
