
using TaskManagement.Application.Common;
using TaskManagement.Application.Users.Abstractions;

using TaskManagement.Application.Users.Dtos;
using TaskManagement.Application.Users.interfaces;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.Enums;
using TaskManagement.Domain.Users.Policies;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.UserCase
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IClock _clock;

        public UserService(IUserRepository userRepositry, IClock clock)
        {
            _repo = userRepositry;
            _clock = clock;
        }

        private async Task<Result<UserDto>> Register
            (UserRegisterDto dto, 
            IPasswordPolicy _passwordPolicy,
            CancellationToken ct)
        {
            var email = Email.Create(dto.Email);

            if (await _repo.GetByEmailAsync(email, ct) is not null)
                return Result<UserDto>.Failure(Errors.User.EmailDuplicated);



            var password = PasswordHash.FromPlainText(dto.Password, _passwordPolicy);
            var fullname = FullName.Create(dto.FirstName, dto.LastName);


            var user = await User.RegisterAsync(
                                                    fullname, 
                                                    email,
                                                    password, 
                                                    Role.Default,
                                                    _clock
                                                );

            await _repo.AddAsync(user, ct);

            return Result<UserDto>.Success(
                new UserDto
                (
                     user.Id,
                     user.Name.Display,
                     user.Email.Value,
                     user.Role.ToString()

                )
                );
        }

        public async Task<Result<UserDto>> RegisterAsync(UserRegisterDto cmd, CancellationToken ct)
        {

            IPasswordPolicy _passwordPolicy = new DefaultPasswordPolicy();
            return await Register(cmd, _passwordPolicy, ct);

        }

        public async Task<Result<UserDto>> LoginAsync(UserLoginDto dto, CancellationToken ct)
        {
            return await Login(dto, ct);
        }

        private async Task<Result<UserDto>> Login(UserLoginDto dto, CancellationToken ct)
        {
            var email = Email.Create(dto.Email);
            var user = await _repo.GetByEmailAsync(email, ct);
            if (user is null)
                return Result<UserDto>.Failure(Errors.User.EmailInvlid);

            if (!user.PasswordHash.Verify(dto.Password))
                return Result<UserDto>.Failure(Errors.User.UserWrongCredential);

            user.RecordLogin(_clock);
            await _repo.UpdateAsync(user, ct);
            return Result<UserDto>.Success(new UserDto
            (
                 user.Id.Value,
                 user.Email.Value,
                 user.Name.Display,
                 user.Role.ToString()

            ));

        }

        private async Task<Result<UserDto>> ChangeRole(ChangeUserRoleDto dto, CancellationToken ct)
        {
            var userId = new UserId(dto.UserId);
            var user = await _repo.GetByIdAsync(userId, ct);

            if (user is null) return Result<UserDto>.Failure(Errors.User.NotFound);

            user.ChangeRole(dto.NewRole, dto.ChangedBy, _clock);

            await _repo.UpdateAsync(user, ct);



            return Result<UserDto>.Success(new UserDto
            (
                user.Id.Value,
                user.Name.Display,
                user.Email.Value,
                user.Role.ToString()
            ));
            
        }

        public async Task<Result<UserDto>> ChangeRoleAsync(ChangeUserRoleDto dto, CancellationToken ct)
        {
            return await ChangeRole(dto, ct);
        }

        private async Task<Result<List<UserDto>>> ListUsers(CancellationToken ct)
        {
            var users = await _repo.ListAsync(ct);
            var dtos = users.Select(u =>
                new UserDto(u.Id.Value, u.Name.Display, u.Email.Value, u.Role.ToString())
            ).ToList();

            return Result<List<UserDto>>.Success(dtos);
           
        }

        public async Task<Result<List<UserDto>>> ListUsersAsync(CancellationToken ct)
        {
            return await ListUsers(ct);
        }

        private async Task<Result<UserDto>> GetUserById(UserId userId, CancellationToken ct)
        {
            var userIdObj = new UserId(userId);
            var user = await _repo.GetByIdAsync(userIdObj, ct);
            if (user is null)
                return Result<UserDto>.Failure(Errors.User.NotFound);
            return Result<UserDto>.Success(new UserDto
            (
                user.Id.Value,
                user.Name.Display,
                user.Email.Value,
                user.Role.ToString()
            ));
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(UserId userId, CancellationToken ct)
        {
            return await GetUserById(userId, ct);
        }
    }
}
