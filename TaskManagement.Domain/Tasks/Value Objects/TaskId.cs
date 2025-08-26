using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ObjectValues;

namespace TaskManagement.Domain.Tasks.Value_Objects
{
    public readonly record struct  TaskId(Guid Value)
    {
        public static TaskId New() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();

        public static implicit operator Guid(TaskId TaskId) => TaskId.Value;

        public static implicit operator TaskId(Guid Value) => new(Value);
    }
}
