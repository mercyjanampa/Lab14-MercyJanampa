using Lab11_MercyJanampa.Application.UseCases.Roles.Commands;
using Lab11_MercyJanampa.Application.UseCases.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_MercyJanampa.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllRolesQuery()
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetRoleByIdQuery(id)
        );

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoleCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(new
        {
            roleId = id,
            message = "Rol creado correctamente"
        });
    }
}