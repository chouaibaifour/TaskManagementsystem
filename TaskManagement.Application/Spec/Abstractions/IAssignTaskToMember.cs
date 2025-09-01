using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Spec.Contracts;
using TaskManagement.Application.Tasks.Contracts;


namespace TaskManagement.Application.Spec.Abstractions
{
    public interface IAssignTaskToMember
    {
        Task<Result<TaskResponse>> AssignTaskToMember(AssignTaskToMemberRequest dto);
    }
}
