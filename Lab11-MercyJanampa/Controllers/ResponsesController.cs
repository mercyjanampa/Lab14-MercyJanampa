using Lab11_MercyJanampa.Application.UseCases.Responses.Commands;
using Lab11_MercyJanampa.Application.UseCases.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_MercyJanampa.Controllers;

[ApiController]
[Route("api/responses")]
public class ResponsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ticket/{ticketId:guid}")]
    public async Task<IActionResult> GetByTicketId(Guid ticketId)
    {
        var result = await _mediator.Send(
            new GetResponsesByTicketIdQuery(ticketId)
        );

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateResponseCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(new
        {
            responseId = id,
            message = "Respuesta creada correctamente"
        });
    }
}