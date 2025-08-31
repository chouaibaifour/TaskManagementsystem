using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Dtos
{
    public record struct ChangeUserPasswordDto(
        string Email,
        string CurrentPassword,
        string NewPassword);


}
