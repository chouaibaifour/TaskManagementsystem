using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;

namespace TaskManagement.Domain.Tasks.ValueObjects
{
    public sealed class TaskStatus
    {

        private enum EnTaskStatus
        {
            ToDo,
            InProgress,
            Completed
        }
        private readonly EnTaskStatus _value;
        public string Display => _value.ToString();

        public static TaskStatus Todo => new(EnTaskStatus.ToDo);
        public static TaskStatus InProgress => new(EnTaskStatus.InProgress);
        public static TaskStatus Completed => new(EnTaskStatus.Completed);
        public static TaskStatus Default => Todo;

        private TaskStatus(EnTaskStatus value)=> _value=value;
        public static bool operator ==(TaskStatus a, TaskStatus b) =>
            (ReferenceEquals(a, b) ||
             a?._value == b?._value);

        public static bool operator !=(TaskStatus a, TaskStatus b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) => 
            obj is TaskStatus status && _value == status._value;

        public override int GetHashCode() => HashCode.Combine(_value);

        internal bool CanTransitionTo(TaskStatus newTaskStatus)
        {
            if (this == newTaskStatus)
                return true;
            
            return CanTransitionToInProgress()||
                   CanTransitionToCompleted()||
                   CanTransitionToTodo();
        }

        private bool CanTransitionToInProgress()
        {
            return (this == Todo);
        }

        private bool CanTransitionToCompleted()
        {
            return this == InProgress;
        }

        private bool CanTransitionToTodo()
        {
            return this == InProgress;
        }

        public static TaskStatus FromEnum(Enum value)
        {
            Enum.TryParse<EnTaskStatus>(value.ToString(), out var status  );
            return new TaskStatus(status);
        }

        public Enum ToEnum() => _value;
    }
}
