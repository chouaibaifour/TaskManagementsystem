using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Projects.Dtos
{
    public record struct UpdateProjectDto(
        Guid Id,
        string Name,
        string Description
        );

}
