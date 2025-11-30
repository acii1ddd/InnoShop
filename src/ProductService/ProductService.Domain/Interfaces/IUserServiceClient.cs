namespace ProductService.Domain.Interfaces;

public interface IUserServiceClient
{
    public Task<bool> GetByIdAsync(Guid userId, CancellationToken ct);
}