using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Dtos
{
    public record struct CreateProjectDto(
        string Name,
        string Description,
        UserId OwnerId
        );

}
