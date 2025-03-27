using LibraryManager.Application.Commands.PenaltyCommands;
using LibraryManager.Application.Queries.PenaltyQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PenaltiesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(InsertPenaltyCommand command)
    {
        var result = await mediator.Send(command);
        
        return !result.IsSuccess
            ? BadRequest(result)
            : Created($"{Request.Path}/{result.Data}", string.Empty);
    }
    
    [HttpGet("user/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPenaltiesByUser(int userId)
    {
        var result = await mediator.Send(new GetPenaltiesByUserQuery(userId));

        return !result.IsSuccess
            ? NotFound(result)
            : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll() =>
        Ok(await mediator.Send(new GetAllPenaltiesQuery()));

    [HttpPatch("{id:int}/pay")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id)
    {
        var result = await mediator.Send(new UpdatePenaltyCommand(id));

        return !result.IsSuccess
            ? BadRequest(result)
            : NoContent();
    }
}