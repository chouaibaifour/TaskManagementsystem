using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Users.Commands.Login;
using TaskManagement.Application.Users.Commands.Register;
using TaskManagement.Application.Users.Queries.GetUserById;
using TaskManagement.Application.Users.Queries.ListUsers;
using TaskManagement.Application.Users.Commands.ChangeRole;
namespace TaskManagement.WebAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken ct)
        {
            
            var result = await _mediator.Send(command, ct);
            return result.IsSuccess
                ? Ok(new { result.Value?.Id, result.Value?.Email })
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Login user and issue JWT
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return result.IsSuccess
                ? Ok(new { Token = result.Value })
                : Unauthorized(result.Error);
        }

        /// <summary>
        /// Change user role (admin only)
        /// </summary>
        [HttpPatch("{id:guid}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(Guid id, [FromBody] ChangeUserRoleCommand command, CancellationToken ct)
        {
            // Ensure route id == body id
            if (id != command.UserId)
                return BadRequest("Mismatched user id");

            var result = await _mediator.Send(command, ct);
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query, ct);
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        /// <summary>
        /// List all users (admin only)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List(CancellationToken ct)
        {
            var result = await _mediator.Send(new ListUsersQuery(), ct);
            return Ok(result.Value);
        }
    }
}
