namespace ProductService.Application.Interfaces;

public interface IUserServiceClient
{
    public Task<bool> ExistsAsync(Guid userId, CancellationToken ct);
}