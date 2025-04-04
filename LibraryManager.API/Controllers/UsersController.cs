using LibraryManager.Application.Commands.UserCommands;
using LibraryManager.Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(InsertUserCommand command)
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
            var result = await mediator.Send(new GetUserByIdQuery(id));
            
            return !result.IsSuccess
                ? NotFound(result)
                : Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, UpdateUserCommand command)
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
            await mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }

        // [HttpPut("login")]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<IActionResult> Login(InsertLoginCommand command)
        // {
        //     var result = await mediator.Send(command);
        //
        //     return !result.IsSuccess
        //         ? Unauthorized(new { message = result.Message })
        //         : Ok(new { token = result.Data });
        // }
        //
        // [HttpPost("password-recovery/request")]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<IActionResult> RequestPasswordRecovery(
        //     PasswordRecoveryRequestCommand command)
        // {
        //     var result = await mediator.Send(command);
        //
        //     return !result.IsSuccess
        //         ? BadRequest(result.Message)
        //         : Ok();
        // }
        //
        // [HttpPost("password-recovery/validation")]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<IActionResult> RequestPasswordRecovery(
        //     PasswordRecoveryValidateCommand command)
        // {
        //     var result = await mediator.Send(command);
        //
        //     return !result.IsSuccess
        //         ? BadRequest(result.Message)
        //         : Ok();
        // }
        //
        // [HttpPost("password-recovery/change")]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<IActionResult> RequestPasswordRecovery(
        //     PasswordRecoveryChangeCommand command)
        // {
        //     var result = await mediator.Send(command);
        //
        //     return !result.IsSuccess
        //         ? BadRequest(result.Message)
        //         : Ok();
        // }
    }
}