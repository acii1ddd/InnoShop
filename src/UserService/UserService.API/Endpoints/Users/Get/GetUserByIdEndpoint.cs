using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.API.EndpointsSettings;
using UserService.Application.Dtos;
using UserService.Application.UseCases.Queries;

namespace UserService.API.Endpoints.Users.Get;

public record GetUserByIdResponse(UserDto User);
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