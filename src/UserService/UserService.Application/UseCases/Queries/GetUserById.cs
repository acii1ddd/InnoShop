using Shared.CQRS;
using Shared.Exceptions;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;

public record GetUserByIdResult(UserEntity User); // todo dto

internal sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        var user = await userRepository.GetById(query.Id, ct);

        if (user is null)
        {
            throw new NotFoundException("User", query.Id);
        }

        return new GetUserByIdResult(user);
    }
}