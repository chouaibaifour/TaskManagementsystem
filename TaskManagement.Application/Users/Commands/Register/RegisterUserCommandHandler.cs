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
using TaskManagement.Domain.Users.Policies;
using System.Xml.Linq;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.Enums;
namespace TaskManagement.Application.Users.Commands.Register
{
    public sealed class RegisterUserCommandHandler:IRequestHandler<RegisterUserCommand,Result<UserDto>>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordPolicy _passwordPolicy;
        public RegisterUserCommandHandler(IUserRepository userRepositry,IPasswordPolicy passwordPolicy) 
        { _repo = userRepositry; _passwordPolicy = passwordPolicy; }

        public async Task<Result<UserDto>> Handle(RegisterUserCommand cmd, CancellationToken ct)
        {
            var email = Email.Create(cmd.Email);

            if (await _repo.GetByEmailAsync(email,ct)is not null)
                return Result<UserDto>.Failure(Errors.User.EmailDuplicated);
            

           
            var password = PasswordHash.FromPlainText(cmd.Password, _passwordPolicy);
            var fullname = FullName.Create(cmd.FirstName, cmd.LastName);


            var user =await User.RegisterAsync(fullname, email, password,Role.Default, DateTime.UtcNow);

            await _repo.AddAsync(user, ct);

            return Result<UserDto>.Success(
                new UserDto
                (
                     user.Id.Value,
                     user.Name.Display,
                     user.Email.Value,
                     user.Role.ToString()
                   
                )
                );


        }
    }
}
