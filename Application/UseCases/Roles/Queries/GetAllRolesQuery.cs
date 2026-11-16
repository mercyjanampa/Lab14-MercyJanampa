using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Roles.Queries;

public record GetAllRolesQuery
    : IRequest<IEnumerable<Role>>;

internal sealed class GetAllRolesQueryHandler
    : IRequestHandler<GetAllRolesQuery, IEnumerable<Role>>
{
    private readonly IRepository<Role> _repository;

    public GetAllRolesQueryHandler(IRepository<Role> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Role>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}