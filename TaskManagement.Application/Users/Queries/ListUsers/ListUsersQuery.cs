using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;

namespace TaskManagement.Application.Users.Queries.ListUsers
{
    public sealed record ListUsersQuery() : IRequest<Result<List<UserDto>>>;
}
