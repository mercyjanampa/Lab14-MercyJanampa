using Lab11_MercyJanampa.Application.UseCases.Reportes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_MercyJanampa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tickets")]
    public async Task<IActionResult> ExportTickets()
    {
        var file = await _mediator.Send(
            new ExportTicketsReportQuery());

        return File(
            file,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Reporte_Tickets.xlsx");
    }

    [HttpGet("respuestas/{ticketId:guid}")]
    public async Task<IActionResult> ExportResponses(
        Guid ticketId)
    {
        var file = await _mediator.Send(
            new ExportResponsesByTicketReportQuery(ticketId));

        return File(
            file,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Reporte_Respuestas_{ticketId}.xlsx");
    }
}