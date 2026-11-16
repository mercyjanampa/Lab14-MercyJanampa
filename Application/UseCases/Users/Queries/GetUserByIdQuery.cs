using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Users.Queries;

public record GetUserByIdQuery(
    Guid UserId
) : IRequest<User?>;

internal sealed class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IRepository<User> _repository;

    public GetUserByIdQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.UserId);
    }
}