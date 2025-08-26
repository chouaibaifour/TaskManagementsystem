using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Users;

namespace TaskManagement.Application.Users.Commands.Login
{
    public record LoginUserCommand (string Email ,string Password) : IRequest<Result<UserDto>>;
    
}
