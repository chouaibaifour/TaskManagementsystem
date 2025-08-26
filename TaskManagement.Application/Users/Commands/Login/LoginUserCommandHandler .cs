using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Users;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Users.ObjectValues;
using TaskManagement.Domain.Users.Repositories;

namespace TaskManagement.Application.Users.Commands.Login
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _repo;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _repo = userRepository;
        }

        public async Task<Result<UserDto>> Handle(LoginUserCommand cmd, CancellationToken ct)
        {
            var email= Email.Create(cmd.Email);
            var user = await _repo.GetByEmailAsync(email, ct);
            if(user is null)
                return Result<UserDto>.Failure(Errors.User.EmailInvlid);

            if(!user.PasswordHash.Verify(cmd.Password)) 
                return Result<UserDto>.Failure(Errors.User.UserWrongCredential);

            user.RecordLogin(DateTime.UtcNow);
            await _repo.UpdateAsync(user, ct);
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
