using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Users.Repositories;

namespace TaskManagement.Application.Users.Queries.ListUsers
{
    public sealed class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, Result<List<UserDto>>>
    {
        private readonly IUserRepository _repo;

        public ListUsersQueryHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<UserDto>>> Handle(ListUsersQuery query, CancellationToken ct)
        {
            var users = await _repo.ListAsync(ct);

            var dtos = users.Select(u =>
                new UserDto(u.Id.Value, u.Name.Display, u.Email.Value, u.Role.ToString())
            ).ToList();

            return Result<List<UserDto>>.Success(dtos);
        }
    }
}
