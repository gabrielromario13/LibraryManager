using LibraryManager.Application.Commands.PenaltyCommands;
using LibraryManager.Application.Commands.ReservationCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(InsertReservationCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess
            ? BadRequest(result)
            : Created($"{Request.Path}/{result.Data}", string.Empty);
    }
    
    [HttpPatch("{id:int}/reserve")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id)
    {
        var result = await mediator.Send(new UpdateReservationCommand(id));

        return !result.IsSuccess
            ? BadRequest(result)
            : NoContent();
    }
}