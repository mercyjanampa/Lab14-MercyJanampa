using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Reportes.Queries;

public record ExportResponsesByTicketReportQuery(
    Guid TicketId
) : IRequest<byte[]>;

internal sealed class ExportResponsesByTicketReportQueryHandler
    : IRequestHandler<
        ExportResponsesByTicketReportQuery,
        byte[]>
{
    private readonly IRepository<Response> _repository;
    private readonly IExcelReportService _excelReportService;

    public ExportResponsesByTicketReportQueryHandler(
        IRepository<Response> repository,
        IExcelReportService excelReportService)
    {
        _repository = repository;
        _excelReportService = excelReportService;
    }

    public async Task<byte[]> Handle(
        ExportResponsesByTicketReportQuery request,
        CancellationToken cancellationToken)
    {
        var responses = await _repository.FindAsync(
            r => r.TicketId == request.TicketId
        );

        return _excelReportService
            .GenerateResponsesReport(responses);
    }
}