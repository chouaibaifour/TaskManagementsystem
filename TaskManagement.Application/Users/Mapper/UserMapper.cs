using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Users.Contracts;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Mapper
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            (
                user.Id.Value,
                user.Name.Display,
                user.Email.Value,
                user.UserRole.ToEnum()
            );
        }
    }
}
