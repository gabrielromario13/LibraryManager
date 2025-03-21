using LibraryManager.Application.Commands.UserCommands;
using LibraryManager.Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    // [Authorize]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        // [AllowAnonymous]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpGet("{id:int}")]
        // [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
        [HttpGet]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // [HttpPut("{id:int}")]
        // public async Task<IActionResult> Put(int id, UpdateUserCommand command)
        // {
        //     var result = await _mediator.Send(command);
        //     if (!result.IsSuccess)
        //     {
        //         return BadRequest(result.Message);
        //     }
        //
        //     return NoContent();
        // }
        //
        // [HttpDelete("{id:int}")]
        // // [Authorize(Roles = "admin")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var result = await _mediator.Send(new DeleteUserCommand(id));
        //     if (!result.IsSuccess)
        //     {
        //         return BadRequest(result.Message);
        //     }
        //
        //     return NoContent();
        // }

        // [HttpPut("login")]
        // [AllowAnonymous]
        // public async Task<IActionResult> Login(InsertLoginCommand command)
        // {
        //     var result = await _mediator.Send(command);
        //
        //     if (!result.IsSuccess)
        //     {
        //         return Unauthorized(new { message = result.Message });
        //     }
        //
        //     return Ok(new { token = result.Data });
        // }
        //
        // [HttpPost("password-recovery/request")]
        // [Authorize(Roles = "user")]
        // public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryRequestCommand command)
        // {
        //     var result = await _mediator.Send(command);
        //     if (!result.IsSuccess)
        //     {
        //         return BadRequest(result.Message);
        //     }
        //
        //     return Ok();
        // }

        // [HttpPost("password-recovery/validation")]
        // [Authorize(Roles = "user")]
        // public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryValidateCommand command)
        // {
        //     var result = await _mediator.Send(command);
        //     if (!result.IsSuccess)
        //     {
        //         return BadRequest(result.Message);
        //     }
        //
        //     return Ok();
        // }
        //
        // [HttpPost("password-recovery/change")]
        // [Authorize(Roles = "user")]
        // public async Task<IActionResult> RequestPasswordRecovery(PasswordRecoveryChangeCommand command)
        // {
        //     var result = await _mediator.Send(command);
        //     if (!result.IsSuccess)
        //     {
        //         return BadRequest(result.Message);
        //     }
        //
        //     return Ok();
        // }
    }
}