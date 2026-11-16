using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Tickets.Commands;

public record CreateTicketCommand(
    Guid UserId,
    string Title,
    string? Description
) : IRequest<Guid>;

internal sealed class CreateTicketCommandHandler
    : IRequestHandler<CreateTicketCommand, Guid>
{
    private readonly IRepository<Ticket> _repository;

    public CreateTicketCommandHandler(IRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateTicketCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = "abierto",
            CreatedAt = DateTime.Now
        };

        await _repository.AddAsync(ticket);
        await _repository.SaveChangesAsync();

        return ticket.TicketId;
    }
}