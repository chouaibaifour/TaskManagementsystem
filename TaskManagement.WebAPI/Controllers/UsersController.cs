using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Application.Users.Contracts;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.WebAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequest dto)
        {
            
            var result = await _userService.RegisterAsync(dto);
            return result.IsSuccess
                ? Ok(result.Value )
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Login user and issue JWT
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLoginResquest dto)
        {
            var result = await _userService.LoginAsync(dto);
            return result.IsSuccess
                ? Ok(new { Token = result.Value })
                : Unauthorized(result.Error);
        }

        /// <summary>
        /// Change user userRole (admin only)
        /// </summary>
        [HttpPatch("{id:guid}/userRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(UserId id,UserChangeRoleRequest dto)
        {
            // Ensure route id == body id
            if (id != dto.UserId)
                return BadRequest("Mismatched user id");

            var result = await _userService.ChangeRoleAsync(dto);
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {

            var result = await _userService.GetUserByIdAsync(id);
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        /// <summary>
        /// List all users (admin only)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var result = await _userService.ListUsersAsync();
            return Ok(result.Value);
        }
    }
}
