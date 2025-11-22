using Shared.CQRS;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Queries;

public sealed record GetUsersQuery(int PageIndex = 1, int PageSize = 10) : IQuery<GetPaginatedUsersResult>;
public sealed record GetPaginatedUsersResult(IEnumerable<UserEntity> Users, int TotalCount, int TotalPages);

public sealed class Get(IUserRepository userRepository) : IQueryHandler<GetUsersQuery, GetPaginatedUsersResult>
{
    public async Task<GetPaginatedUsersResult> Handle(GetUsersQuery query, CancellationToken ct)
    {
       var users =  await userRepository
            .GetPagedAsync(query.PageIndex, query.PageSize, ct);

        var totalCount = await userRepository.GetCountAsync(ct);

        var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);
        
        return new GetPaginatedUsersResult(users, totalCount, totalPages);
    }
}