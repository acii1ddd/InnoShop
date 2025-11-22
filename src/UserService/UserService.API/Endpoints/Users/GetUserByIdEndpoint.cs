using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.UseCases.Queries;
using UserService.Domain.Entities;

namespace UserService.API.Endpoints.Users;

// todo dto
public record GetUserByIdResponse(UserEntity User);
public class GetUserByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}", async (
            ISender sender, 
            [FromRoute] Guid id, 
            CancellationToken ct) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(id), ct);

            var response = result.Adapt<GetUserByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetUserById")
        .Produces<GetUserByIdResponse>()
        .WithSummary("Get user by specified id");;
    }
}