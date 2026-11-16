using Lab11_MercyJanampa.Domain.Entities;
using Lab11_MercyJanampa.Domain.Interfaces;
using MediatR;

namespace Lab11_MercyJanampa.Application.UseCases.Reportes.Queries;

public record ExportTicketsReportQuery : IRequest<byte[]>;

internal sealed class ExportTicketsReportQueryHandler
    : IRequestHandler<ExportTicketsReportQuery, byte[]>
{
    private readonly IRepository<Ticket> _repository;
    private readonly IExcelReportService _excelReportService;

    public ExportTicketsReportQueryHandler(
        IRepository<Ticket> repository,
        IExcelReportService excelReportService)
    {
        _repository = repository;
        _excelReportService = excelReportService;
    }

    public async Task<byte[]> Handle(
        ExportTicketsReportQuery request,
        CancellationToken cancellationToken)
    {
        var tickets = await _repository.GetAllAsync();

        return _excelReportService
            .GenerateTicketsReport(tickets);
    }
}