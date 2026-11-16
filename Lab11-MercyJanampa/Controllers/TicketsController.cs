using Lab11_MercyJanampa.Application.UseCases.Tickets.Commands;
using Lab11_MercyJanampa.Application.UseCases.Tickets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_MercyJanampa.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllTicketsQuery()
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetTicketByIdQuery(id)
        );

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTicketCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(new
        {
            ticketId = id,
            message = "Ticket creado correctamente"
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateTicketCommand command)
    {
        var request = command with
        {
            TicketId = id
        };

        await _mediator.Send(request);

        return Ok(new
        {
            message = "Ticket actualizado correctamente"
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(
            new DeleteTicketCommand(id)
        );

        return Ok(new
        {
            message = "Ticket eliminado correctamente"
        });
    }
}