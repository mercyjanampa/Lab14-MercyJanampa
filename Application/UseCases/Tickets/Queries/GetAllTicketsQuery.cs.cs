using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Tickets.Queries;

public record GetAllTicketsQuery : IRequest<IEnumerable<Ticket>>;

internal sealed class GetAllTicketsQueryHandler
    : IRequestHandler<GetAllTicketsQuery, IEnumerable<Ticket>>
{
    private readonly IRepository<Ticket> _repository;

    public GetAllTicketsQueryHandler(IRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Ticket>> Handle(
        GetAllTicketsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}