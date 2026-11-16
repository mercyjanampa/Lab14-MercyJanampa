using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Tickets.Queries;

public record GetTicketByIdQuery(
    Guid TicketId
) : IRequest<Ticket?>;

internal sealed class GetTicketByIdQueryHandler
    : IRequestHandler<GetTicketByIdQuery, Ticket?>
{
    private readonly IRepository<Ticket> _repository;

    public GetTicketByIdQueryHandler(IRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task<Ticket?> Handle(
        GetTicketByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.TicketId);
    }
}