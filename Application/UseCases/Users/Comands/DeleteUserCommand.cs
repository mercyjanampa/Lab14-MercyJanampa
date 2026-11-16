using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Users.Commands;

public record DeleteUserCommand(
    Guid UserId
) : IRequest<Unit>;

internal sealed class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IRepository<User> _repository;

    public DeleteUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId);

        if (user is null)
            throw new KeyNotFoundException("Usuario no encontrado.");

        _repository.Delete(user);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}