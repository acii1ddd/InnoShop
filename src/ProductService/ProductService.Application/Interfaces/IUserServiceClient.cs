using ProductService.Application.ExternalDtos;

namespace ProductService.Application.Interfaces;

public interface IUserServiceClient
{
    public Task<bool> ExistsAsync(Guid userId, CancellationToken ct);
    
    public Task<UserResponseDto?> GetByIdAsync(Guid userId, CancellationToken ct);
    
    public Task<List<UserResponseDto>> GetAllAsync(IEnumerable<Guid> userIds, CancellationToken ct);
}
