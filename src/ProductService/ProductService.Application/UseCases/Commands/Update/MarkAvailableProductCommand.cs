using FluentValidation;
using MediatR;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Commands.Update;

public sealed record MarkAvailableProductCommand(Guid ProductId) 
    : ICommand;

public class MarkAvailableProductValidator
    : AbstractValidator<MarkAvailableProductCommand>
{
    public MarkAvailableProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Id is required");
    }
}
public class MarkAvailableProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<MarkAvailableProductCommand>
{
    public async Task<Unit> Handle(MarkAvailableProductCommand command, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId, ct);

        if (product is null)
        {
            throw new NotFoundException("Product", command.ProductId);
        }
        
        product.MarkAvailable();
        
        await productRepository.UpdateAsync(product, ct);
        
        return Unit.Value;
    }
}