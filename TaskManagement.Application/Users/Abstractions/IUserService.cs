
using TaskManagement.Application.Common;
using TaskManagement.Application.Users.Dtos;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Abstractions
{
    public interface IUserService
    {
        Task<Result<UserDto>> RegisterAsync(UserRegisterDto dto, CancellationToken ct);
        Task<Result<UserDto>> LoginAsync(UserLoginDto dto, CancellationToken ct);
        Task<Result<UserDto>> ChangeRoleAsync(ChangeUserRoleDto dto, CancellationToken ct);
        Task<Result<UserDto>> GetUserByIdAsync(UserId UserId, CancellationToken ct);
        Task<Result<List<UserDto>>> ListUsersAsync(CancellationToken ct);
    }
}
