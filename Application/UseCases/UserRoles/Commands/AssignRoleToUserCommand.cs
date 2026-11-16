using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.UserRoles.Commands;

public record AssignRoleToUserCommand(
    Guid UserId,
    Guid RoleId
) : IRequest<Unit>;

internal sealed class AssignRoleToUserCommandHandler
    : IRequestHandler<AssignRoleToUserCommand, Unit>
{
    private readonly IRepository<UserRole> _repository;

    public AssignRoleToUserCommandHandler(
        IRepository<UserRole> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        AssignRoleToUserCommand request,
        CancellationToken cancellationToken)
    {
        var userRole = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = DateTime.Now
        };

        await _repository.AddAsync(userRole);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}