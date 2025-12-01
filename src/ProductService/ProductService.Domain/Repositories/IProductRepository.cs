using ProductService.Domain.Entities;

namespace ProductService.Domain.Repositories;

public interface IProductRepository : IAsyncRepository<ProductEntity>
{
    
}