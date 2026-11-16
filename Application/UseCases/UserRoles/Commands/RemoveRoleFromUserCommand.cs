using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.UserRoles.Commands;

public record RemoveRoleFromUserCommand(
    Guid UserId,
    Guid RoleId
) : IRequest<Unit>;

internal sealed class RemoveRoleFromUserCommandHandler
    : IRequestHandler<RemoveRoleFromUserCommand, Unit>
{
    private readonly IRepository<UserRole> _repository;

    public RemoveRoleFromUserCommandHandler(
        IRepository<UserRole> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        RemoveRoleFromUserCommand request,
        CancellationToken cancellationToken)
    {
        var userRole = await _repository.FirstOrDefaultAsync(
            ur => ur.UserId == request.UserId
                  && ur.RoleId == request.RoleId
        );

        if (userRole is null)
            throw new KeyNotFoundException(
                "La asignación de rol no fue encontrada."
            );

        _repository.Delete(userRole);

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}