
using TaskManagement.Application.Common;
using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Application.Users.Contracts;
using TaskManagement.Application.Users.Mapper;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.Policies;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.UseCase
{
    public class UserService(IUserRepository userRepository, IClock clock) : IUserService
    {
        private async Task<Result<UserResponse>> Register
            (UserRegisterRequest dto, 
            IPasswordPolicy passwordPolicy
            )
        {
           

            if (await userRepository.GetByEmailAsync(dto.Email) is not null)
                return Result<UserResponse>.Failure(DomainErrors.User.EmailDuplicated);


            var email = Email.Create(dto.Email);
            var password = PasswordHash.FromPlainText(dto.Password, passwordPolicy);
            var fullname = FullName.Create(dto.FirstName, dto.LastName);


            var user =  User.RegisterAsync(
                                                    fullname, 
                                                    email,
                                                    password, 
                                                    UserRole.Default,
                                                    clock
                                                );

            await userRepository.AddAsync(user);

            return Result<UserResponse>.Success(user.ToResponse());
        }

        public async Task<Result<UserResponse>> RegisterAsync(UserRegisterRequest dto)
        {

            IPasswordPolicy passwordPolicy = new DefaultPasswordPolicy();
            return await Register(dto, passwordPolicy);

        }

        public async Task<Result<UserResponse>> LoginAsync(UserLoginResquest dto)
        {
            return await Login(dto);
        }

        private async Task<Result<UserResponse>> Login(UserLoginResquest dto)
        {
           
            var user = await userRepository.GetByEmailAsync(dto.Email);
            if (user is null)
                return Result<UserResponse>.Failure(DomainErrors.User.EmailInvlid);

            if (!user.PasswordHash.Verify(dto.Password))
                return Result<UserResponse>.Failure(DomainErrors.User.UserWrongCredential);

            user.RecordLogin(clock);
            await userRepository.UpdateAsync(user);
            return Result<UserResponse>.Success(user.ToResponse());

        }

        private async Task<Result<UserResponse>> ChangeRole(UserChangeRoleRequest dto)
        {
            var userId = new UserId(dto.UserId);
            var user = await userRepository.GetByIdAsync(userId);

            if (user is null) return Result<UserResponse>.Failure(DomainErrors.User.NotFound);

            user.ChangeRole(dto.NewUserRole, dto.ChangedBy, clock);

            await userRepository.UpdateAsync(user);



            return Result<UserResponse>.Success(user.ToResponse());
            
        }

        public async Task<Result<UserResponse>> ChangeRoleAsync(UserChangeRoleRequest dto)
        {
            return await ChangeRole(dto);
        }

        private async Task<Result<List<UserResponse>>> ListUsers()
        {
            var users = await userRepository.ListAsync();
            

            return Result<List<UserResponse>>.Success(users.Select(u =>u.ToResponse()).ToList());
           
        }

        public async Task<Result<List<UserResponse>>> ListUsersAsync()
        {
            return await ListUsers();
        }

        private async Task<Result<UserResponse>> GetUserById(Guid userId)
        {
            var userIdObj = new UserId(userId);
            var user = await userRepository.GetByIdAsync(userIdObj);
            if (user is null)
                return Result<UserResponse>.Failure(DomainErrors.User.NotFound);
            return Result<UserResponse>.Success(user.ToResponse());
        }

        public async Task<Result<UserResponse>> GetUserByIdAsync(Guid userId)
        {
            return await GetUserById(userId );
        }

        public async Task<Result<UserResponse>> ChangePasswordAsync(UserChangePasswordRequest dto)
        {
            IPasswordPolicy passwordPolicy = new DefaultPasswordPolicy();
            return  await ChangePassword(dto , passwordPolicy);
        }

        private async Task<Result<UserResponse>> ChangePassword(UserChangePasswordRequest dto,IPasswordPolicy passwordPolicy)
        {
            

            var user = await userRepository.GetByEmailAsync(dto.Email);


            if (user is null)

                return Result<UserResponse>.Failure(DomainErrors.User.EmailInvlid);


            if (!user.PasswordHash.Verify(dto.CurrentPassword))

                return Result<UserResponse>.Failure(DomainErrors.User.UserWrongCredential);


            user.ChangePassword(dto.NewPassword, passwordPolicy);


            return Result<UserResponse>.Success(user.ToResponse());
        }
    }
}
