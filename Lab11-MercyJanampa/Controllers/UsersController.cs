using Lab11_MercyJanampa.Application.UseCases.Users.Commands;
using Lab11_MercyJanampa.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_MercyJanampa.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllUsersQuery()
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetUserByIdQuery(id)
        );

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(new
        {
            userId = id,
            message = "Usuario creado correctamente"
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateUserCommand command)
    {
        var request = command with
        {
            UserId = id
        };

        await _mediator.Send(request);

        return Ok(new
        {
            message = "Usuario actualizado correctamente"
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(
            new DeleteUserCommand(id)
        );

        return Ok(new
        {
            message = "Usuario eliminado correctamente"
        });
    }
}