using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Users.Commands;

public record UpdateUserCommand(
    Guid UserId,
    string Username,
    string? Email
) : IRequest<Unit>;

internal sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IRepository<User> _repository;

    public UpdateUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId);

        if (user is null)
            throw new KeyNotFoundException("Usuario no encontrado.");

        user.Username = request.Username;
        user.Email = request.Email;

        _repository.Update(user);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}