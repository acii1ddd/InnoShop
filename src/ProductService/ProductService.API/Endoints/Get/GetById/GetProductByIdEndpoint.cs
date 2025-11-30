using Mapster;
using MediatR;
using ProductService.API.EndpointsSettings;
using ProductService.Application.Dtos;
using ProductService.Application.UseCases.Queries.GetById;

namespace ProductService.API.Endoints.Get.GetById;

public record GetProductByIdResponse(ProductWithUserDto Product);

public class GetProductByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{productId:guid}/details", async (
            Guid productId,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(productId), ct);

            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>()
        .WithSummary("Get a product info by specified id")
        .AllowAnonymous();
    }
}