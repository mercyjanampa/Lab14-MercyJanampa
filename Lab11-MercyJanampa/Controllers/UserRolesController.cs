using Lab11_MercyJanampa.Application.UseCases.UserRoles.Commands;
using Lab11_MercyJanampa.Application.UseCases.UserRoles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_MercyJanampa.Controllers;

[ApiController]
[Route("api/userroles")]
public class UserRolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var result = await _mediator.Send(
            new GetUserRolesByUserIdQuery(userId)
        );

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(
        [FromBody] AssignRoleToUserCommand command)
    {
        await _mediator.Send(command);

        return Ok(new
        {
            message = "Rol asignado al usuario correctamente"
        });
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveRole(
        [FromBody] RemoveRoleFromUserCommand command)
    {
        await _mediator.Send(command);

        return Ok(new
        {
            message = "Rol retirado del usuario correctamente"
        });
    }
}