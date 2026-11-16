using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Roles.Queries;

public record GetRoleByIdQuery(
    Guid RoleId
) : IRequest<Role?>;

internal sealed class GetRoleByIdQueryHandler
    : IRequestHandler<GetRoleByIdQuery, Role?>
{
    private readonly IRepository<Role> _repository;

    public GetRoleByIdQueryHandler(IRepository<Role> repository)
    {
        _repository = repository;
    }

    public async Task<Role?> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.RoleId);
    }
}