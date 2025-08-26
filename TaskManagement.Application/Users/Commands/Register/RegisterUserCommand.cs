using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Users;
using MediatR;

namespace TaskManagement.Application.Users.Commands.Register
{
    public record RegisterUserCommand
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    ):IRequest<Result<UserDto>>;
}
