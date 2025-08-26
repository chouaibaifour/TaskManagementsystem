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
namespace TaskManagement.Application.Users.Commands.ChangeRole
{
    public sealed class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, Result<UserDto>>
    {
        private readonly IUserRepository _repo;
        public ChangeUserRoleCommandHandler(IUserRepository userRepository)
        {
            _repo = userRepository;
        }

        public async Task<Result<UserDto>> Handle(ChangeUserRoleCommand cmd, CancellationToken ct)
        {
            var userId = new UserId(cmd.UserId);
            var user = await _repo.GetByIdAsync(userId, ct);

            if(user is null) return Result<UserDto>.Failure(Errors.User.NotFound);

            user.ChangeRole(cmd.NewRole, cmd.ChangedBy);

            await _repo.UpdateAsync(user, ct);



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
