using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;

namespace TaskManagement.Application.Projects.Contracts
{
    public record struct UpdateProjectRequest(
        Guid Id,
        string Name,
        string Description
        );

}
