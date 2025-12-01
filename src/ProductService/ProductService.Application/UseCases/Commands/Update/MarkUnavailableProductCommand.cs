using FluentValidation;
using MediatR;
using ProductService.Domain.Repositories;
using Shared.CQRS;
using Shared.Exceptions;

namespace ProductService.Application.UseCases.Commands.Update;

public sealed record MarkUnavailableProductCommand(Guid ProductId) 
    : ICommand;

public class MarkUnavailableProductValidator
    : AbstractValidator<MarkUnavailableProductCommand>
{
    public MarkUnavailableProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Id is required");
    }
}
internal sealed class UpdateProductStatusCommandHandler(IProductRepository productRepository)
    : ICommandHandler<MarkUnavailableProductCommand>
{

    public async Task<Unit> Handle(MarkUnavailableProductCommand command, CancellationToken ct)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId, ct);

        if (product is null)
        {
            throw new NotFoundException("Product", command.ProductId);
        }
        
        product.MarkUnavailable();
        
        await productRepository.UpdateAsync(product, ct);
        
        return Unit.Value;
    }
}