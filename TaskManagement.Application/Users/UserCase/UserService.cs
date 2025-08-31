
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
            IPasswordPolicy _passwordPolicy
            )
        {
            var email = Email.Create(dto.Email);

            if (await _repo.GetByEmailAsync(email) is not null)
                return Result<UserDto>.Failure(DomainErrors.User.EmailDuplicated);



            var password = PasswordHash.FromPlainText(dto.Password, _passwordPolicy);
            var fullname = FullName.Create(dto.FirstName, dto.LastName);


            var user = await User.RegisterAsync(
                                                    fullname, 
                                                    email,
                                                    password, 
                                                    Role.Default,
                                                    _clock
                                                );

            await _repo.AddAsync(user);

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

        public async Task<Result<UserDto>> RegisterAsync(UserRegisterDto dto)
        {

            IPasswordPolicy _passwordPolicy = new DefaultPasswordPolicy();
            return await Register(dto, _passwordPolicy);

        }

        public async Task<Result<UserDto>> LoginAsync(UserLoginDto dto)
        {
            return await Login(dto);
        }

        private async Task<Result<UserDto>> Login(UserLoginDto dto)
        {
            var email = Email.Create(dto.Email);
            var user = await _repo.GetByEmailAsync(email);
            if (user is null)
                return Result<UserDto>.Failure(DomainErrors.User.EmailInvlid);

            if (!user.PasswordHash.Verify(dto.Password))
                return Result<UserDto>.Failure(DomainErrors.User.UserWrongCredential);

            user.RecordLogin(_clock);
            await _repo.UpdateAsync(user);
            return Result<UserDto>.Success(new UserDto
            (
                 user.Id.Value,
                 user.Email.Value,
                 user.Name.Display,
                 user.Role.ToString()

            ));

        }

        private async Task<Result<UserDto>> ChangeRole(ChangeUserRoleDto dto)
        {
            var userId = new UserId(dto.UserId);
            var user = await _repo.GetByIdAsync(userId);

            if (user is null) return Result<UserDto>.Failure(DomainErrors.User.NotFound);

            user.ChangeRole(dto.NewRole, dto.ChangedBy, _clock);

            await _repo.UpdateAsync(user);



            return Result<UserDto>.Success(new UserDto
            (
                user.Id.Value,
                user.Name.Display,
                user.Email.Value,
                user.Role.ToString()
            ));
            
        }

        public async Task<Result<UserDto>> ChangeRoleAsync(ChangeUserRoleDto dto)
        {
            return await ChangeRole(dto);
        }

        private async Task<Result<List<UserDto>>> ListUsers()
        {
            var users = await _repo.ListAsync();
            var dtos = users.Select(u =>
                new UserDto(u.Id.Value, u.Name.Display, u.Email.Value, u.Role.ToString())
            ).ToList();

            return Result<List<UserDto>>.Success(dtos);
           
        }

        public async Task<Result<List<UserDto>>> ListUsersAsync()
        {
            return await ListUsers();
        }

        private async Task<Result<UserDto>> GetUserById(UserId userId)
        {
            var userIdObj = new UserId(userId);
            var user = await _repo.GetByIdAsync(userIdObj);
            if (user is null)
                return Result<UserDto>.Failure(DomainErrors.User.NotFound);
            return Result<UserDto>.Success(new UserDto
            (
                user.Id.Value,
                user.Name.Display,
                user.Email.Value,
                user.Role.ToString()
            ));
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(UserId userId)
        {
            return await GetUserById(userId );
        }

        public async Task<Result<UserDto>> ChangePasswordAsync(ChangeUserPasswordDto dto)
        {
            IPasswordPolicy passwordPolicy = new DefaultPasswordPolicy();
            return  await ChangePassword(dto , passwordPolicy);
        }

        public async Task<Result<UserDto>> ChangePassword(ChangeUserPasswordDto dto,IPasswordPolicy passwordPolicy)
        {
            var email = Email.Create(dto.Email);
            var user = await _repo.GetByEmailAsync(email);
            if (user is null)
                return Result<UserDto>.Failure(DomainErrors.User.EmailInvlid);

            if (!user.PasswordHash.Verify(dto.CurrentPassword))
                return Result<UserDto>.Failure(DomainErrors.User.UserWrongCredential);
            user.ChangePassword(dto.NewPassword, passwordPolicy);
            return Result<UserDto>.Success(new UserDto
            (
                 user.Id.Value,
                 user.Email.Value,
                 user.Name.Display,
                 user.Role.ToString()
            ));
        }
    }
}
