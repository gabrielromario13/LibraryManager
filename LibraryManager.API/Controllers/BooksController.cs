using LibraryManager.Application.Commands.BookCommands;
using LibraryManager.Application.Queries.BookQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers;

[Route("api/Books")]
[ApiController]
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(InsertBookCommand command)
    {
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetBookByIdQuery(id));

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllBooksQuery();

        var result = await mediator.Send(query);
        
        return Ok(result);
    }

    [HttpPut]
    // [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(/*int id, */UpdateBookCommand command)
    {
        var result = await mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteBookCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}