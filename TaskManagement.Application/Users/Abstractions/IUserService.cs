
using TaskManagement.Application.Common;
using TaskManagement.Application.Users.Contracts;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Abstractions
{
    public interface IUserService
    {
        Task<Result<UserResponse>> RegisterAsync(UserRegisterRequest dto);
        Task<Result<UserResponse>> LoginAsync(UserLoginResquest dto);
        Task<Result<UserResponse>> ChangeRoleAsync(UserChangeRoleRequest dto);
        Task<Result<UserResponse>> ChangePasswordAsync(UserChangePasswordRequest dto);
        Task<Result<UserResponse>> GetUserByIdAsync(Guid Guid);
        Task<Result<List<UserResponse>>> ListUsersAsync();
    }
}
