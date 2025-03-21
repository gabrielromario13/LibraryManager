using LibraryManager.Application.Commands.LoanCommands;
using LibraryManager.Application.Queries.LoanQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
    [Route("api/loans")]
    [ApiController]
    public class LoansController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(InsertLoanCommand command)
        {
            var result = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetLoanByIdQuery(id));
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllLoansQuery();

            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteLoanCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}