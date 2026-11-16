using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Responses.Queries;

public record GetResponsesByTicketIdQuery(
    Guid TicketId
) : IRequest<IEnumerable<Response>>;

internal sealed class GetResponsesByTicketIdQueryHandler
    : IRequestHandler<
        GetResponsesByTicketIdQuery,
        IEnumerable<Response>>
{
    private readonly IRepository<Response> _repository;

    public GetResponsesByTicketIdQueryHandler(
        IRepository<Response> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Response>> Handle(
        GetResponsesByTicketIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.FindAsync(
            r => r.TicketId == request.TicketId
        );
    }
}