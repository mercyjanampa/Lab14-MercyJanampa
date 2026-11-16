using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Tickets.Commands;

public record DeleteTicketCommand(
    Guid TicketId
) : IRequest<Unit>;

internal sealed class DeleteTicketCommandHandler
    : IRequestHandler<DeleteTicketCommand, Unit>
{
    private readonly IRepository<Ticket> _repository;

    public DeleteTicketCommandHandler(IRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeleteTicketCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId);

        if (ticket is null)
            throw new KeyNotFoundException("Ticket no encontrado.");

        _repository.Delete(ticket);

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}