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
                ? BadRequest(result)
                : Created($"{Request.Path}/{result.Data}", string.Empty);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetLoanByIdQuery(id));

            return !result.IsSuccess
                ? NotFound(result)
                : Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await mediator.Send(new GetAllLoansQuery()));
        
        [HttpGet("user/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLoansByUser(int userId)
        {
            var result = await mediator.Send(new GetLoansByUserQuery(userId));

            return !result.IsSuccess
                ? NotFound(result)
                : Ok(result);
        }

        [HttpPut("{id:int}/return")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReturnDate(int id)
        {
            var result = await mediator.Send(new UpdateLoanCommand(id));

            return !result.IsSuccess
                ? BadRequest(result)
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