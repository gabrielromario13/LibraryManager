using LibraryManager.Application.Commands.LoanCommands;
using LibraryManager.Application.Queries.LoanQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(InsertLoanCommand command)
        {
            var result = await mediator.Send(command);

            return !result.IsSuccess
                ? BadRequest(result.Message)
                : Created($"{Request.Path}/{result.Data}", command);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetLoanByIdQuery(id));

            return !result.IsSuccess
                ? NotFound(result.Message)
                : Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await mediator.Send(new GetAllLoansQuery()));

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReturnDate(int id)
        {
            var result = await mediator.Send(new UpdateLoanCommand(id));

            return !result.IsSuccess
                ? BadRequest(result.Message)
                : NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteLoanCommand(id));
            return NoContent();
        }
    }
}