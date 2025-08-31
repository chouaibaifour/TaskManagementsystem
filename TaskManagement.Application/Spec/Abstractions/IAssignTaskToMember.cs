using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Spec.Dtos;
using TaskManagement.Application.Tasks.Dtos;

namespace TaskManagement.Application.Spec.Abstractions
{
    public interface IAssignTaskToMember
    {
        Task<Result<TaskDto>> AssignTaskToMember(AssignTaskToMemberDto dto);
    }
}
