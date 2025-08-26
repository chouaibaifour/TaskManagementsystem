using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Users.ObjectValues;
using TaskManagement.Domain.Users.Repositories;

namespace TaskManagement.Application.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _repo;

        public GetUserByIdQueryHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken ct)
        {
            var userId = new UserId(query.UserId);
            var user = await _repo.GetByIdAsync(userId, ct);

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
    }
}
