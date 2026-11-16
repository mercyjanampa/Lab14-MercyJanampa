using Lab11_MercyJanampa.Domain.Entities;

namespace Lab11_MercyJanampa.Domain.Interfaces;

public interface IExcelReportService
{
    byte[] GenerateTicketsReport(IEnumerable<Ticket> tickets);

    byte[] GenerateResponsesReport(IEnumerable<Response> responses);
}