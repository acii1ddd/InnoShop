using UserService.Application.ExternalDtos;

namespace UserService.Application.Interfaces;

public interface IProductServiceClient
{
    public Task<bool> MarkProductAsAvailableAsync(Guid productId, CancellationToken ct);

    public Task<bool> MarkProductAsUnavailableAsync(Guid productId, CancellationToken ct);
    
    public Task<IReadOnlyList<ProductWithUserResponseDto>> GetAvailableProductsByUserIdAsync(
        Guid userId,
        CancellationToken ct);

    public Task<IReadOnlyList<ProductWithUserResponseDto>> GetUnavailableProductsByUserIdAsync(
        Guid userId,
        CancellationToken ct);
}
