using LibraryManager.Application.Commands.PenaltyCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(InsertPenaltyCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess
            ? BadRequest(result)
            : Created($"{Request.Path}/{result.Data}", string.Empty);
    }
}