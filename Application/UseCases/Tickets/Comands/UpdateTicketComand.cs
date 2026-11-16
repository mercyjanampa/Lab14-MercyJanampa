using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Tickets.Commands;

public record UpdateTicketCommand(
    Guid TicketId,
    string Title,
    string? Description,
    string Status
) : IRequest<Unit>;

internal sealed class UpdateTicketCommandHandler
    : IRequestHandler<UpdateTicketCommand, Unit>
{
    private readonly IRepository<Ticket> _repository;

    public UpdateTicketCommandHandler(IRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        UpdateTicketCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId);

        if (ticket is null)
            throw new KeyNotFoundException("Ticket no encontrado.");

        ticket.Title = request.Title;
        ticket.Description = request.Description;
        ticket.Status = request.Status;

        if (request.Status == "cerrado")
        {
            ticket.ClosedAt = DateTime.Now;
        }

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}