using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Responses.Commands;

public record CreateResponseCommand(
    Guid TicketId,
    Guid ResponderId,
    string Message
) : IRequest<Guid>;

internal sealed class CreateResponseCommandHandler
    : IRequestHandler<CreateResponseCommand, Guid>
{
    private readonly IRepository<Response> _repository;

    public CreateResponseCommandHandler(
        IRepository<Response> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateResponseCommand request,
        CancellationToken cancellationToken)
    {
        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message,
            CreatedAt = DateTime.SpecifyKind(
                DateTime.Now,
                DateTimeKind.Unspecified
            )
        };

        await _repository.AddAsync(response);
        await _repository.SaveChangesAsync();

        return response.ResponseId;
    }
}