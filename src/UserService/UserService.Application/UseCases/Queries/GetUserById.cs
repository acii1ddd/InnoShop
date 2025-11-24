using Mapster;
using Shared.CQRS;
using Shared.Exceptions;
using UserService.Application.Dtos;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Queries;

public sealed record GetUserByIdQuery(Guid Id) 
    : IQuery<GetUserByIdResult>;

public record GetUserByIdResult(UserDto User);

internal sealed class GetUserByIdQueryHandler(IUserRepository userRepository) 
    : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(query.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", query.Id);
        }

        var userDto = user.Adapt<UserDto>();
        return new GetUserByIdResult(userDto);
    }
}