using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Users.Enums;
namespace TaskManagement.Application.Users.Commands.ChangeRole
{
    public sealed record ChangeUserRoleCommand(Guid UserId,Role NewRole,string ChangedBy):IRequest<Result<UserDto>>;
}
