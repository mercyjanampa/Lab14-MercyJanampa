using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Users.Queries;

public record GetAllUsersQuery
    : IRequest<IEnumerable<User>>;

internal sealed class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IRepository<User> _repository;

    public GetAllUsersQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}