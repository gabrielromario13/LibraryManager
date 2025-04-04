using LibraryManager.Application.Commands.BookCommands;
using LibraryManager.Application.Queries.BookQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace LibraryManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(InsertBookCommand command)
    {
        var result = await mediator.Send(command);

        return !result.IsSuccess
            ? BadRequest(result)
            : Created($"{Request.Path}/{result.Data}", string.Empty);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetBookByIdQuery(id));

        return !result.IsSuccess
            ? NotFound(result)
            : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll() =>
        Ok(await mediator.Send(new GetAllBooksQuery()));
    
    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAvailable() =>
        Ok(await mediator.Send(new GetAllAvailableBooksQuery()));

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, UpdateBookCommand command)
    {
        command.Id = id;

        var result = await mediator.Send(command);

        return !result.IsSuccess
            ? BadRequest(result)
            : NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteBookCommand(id));
        return NoContent();
    }
}