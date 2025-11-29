using MediatR;
using Shared.CQRS;

namespace UserService.Application.UseCases.Commands.Recovery;

public sealed record ForgotPasswordCommand(string Email) : ICommand;

internal sealed class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
{
    public Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}