using MediatR;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Commands.Update;

public sealed record UpdateProductInfoCommand(string Name, string Description, decimal Price, Guid ProductId)
    : ICommand;

internal sealed class UpdateProductCommandHandler(IProductRepository productRepository) 
    : ICommandHandler<UpdateProductInfoCommand>
{
    public async Task<Unit> Handle(UpdateProductInfoCommand command, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId, ct);

        if (product is null)
        {
            throw new NotFoundException("Product", command.ProductId);
        }
        
        product.ChangeName(command.Name);
        product.ChangeDescription(command.Description);
        product.ChangePrice(command.Price);

        await productRepository.UpdateAsync(product, ct);
        
        return Unit.Value;
    }
}