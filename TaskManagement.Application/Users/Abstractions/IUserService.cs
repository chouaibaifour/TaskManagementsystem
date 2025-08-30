
using TaskManagement.Application.Common;
using TaskManagement.Application.Users.Dtos;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Abstractions
{
    public interface IUserService
    {
        Task<Result<UserDto>> RegisterAsync(UserRegisterDto dto);
        Task<Result<UserDto>> LoginAsync(UserLoginDto dto);
        Task<Result<UserDto>> ChangeRoleAsync(ChangeUserRoleDto dto);
        Task<Result<UserDto>> GetUserByIdAsync(UserId UserId);
        Task<Result<List<UserDto>>> ListUsersAsync();
    }
}
