using Mapster;
using MediatR;
using ProductService.API.EndpointsSettings;
using ProductService.Application.UseCases.Commands;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Tools;

namespace ProductService.API.Endoints.Post;

public sealed record CreateProductRequest(string Name, string Description, decimal Price);

public sealed record CreateProductResponse(Guid UserId);

public class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (
            CreateProductRequest request,
            ISender sender,
            IUserContext userContext,
            CancellationToken ct) =>
        {
            var userId = userContext.GetUserId();
            
            var command = new CreateProductCommand(request.Name, request.Description, request.Price, userId);
            
            var result = await sender.Send(command, ct);

            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"api/products/{response.UserId}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .WithSummary("Creates a product with provided information")
        .RequireAuthorization("Default");
    }
}