using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Roles.Commands;

public record CreateRoleCommand(
    string RoleName
) : IRequest<Guid>;

internal sealed class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IRepository<Role> _repository;

    public CreateRoleCommandHandler(IRepository<Role> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = new Role
        {
            RoleId = Guid.NewGuid(),
            RoleName = request.RoleName
        };

        await _repository.AddAsync(role);
        await _repository.SaveChangesAsync();

        return role.RoleId;
    }
}