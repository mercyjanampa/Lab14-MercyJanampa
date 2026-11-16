using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.UserRoles.Queries;

public record GetUserRolesByUserIdQuery(
    Guid UserId
) : IRequest<IEnumerable<UserRole>>;

internal sealed class GetUserRolesByUserIdQueryHandler
    : IRequestHandler<
        GetUserRolesByUserIdQuery,
        IEnumerable<UserRole>>
{
    private readonly IRepository<UserRole> _repository;

    public GetUserRolesByUserIdQueryHandler(
        IRepository<UserRole> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserRole>> Handle(
        GetUserRolesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.FindAsync(
            ur => ur.UserId == request.UserId
        );
    }
}