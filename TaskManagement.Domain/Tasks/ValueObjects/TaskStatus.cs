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
        public string Value { get; private set; }
        public byte Number { get; private set; }
        public string Display => Value;

        public static TaskStatus Todo => new("To do", 1);
        public static TaskStatus InProgress => new("InProgress", 2);
        public static TaskStatus Completed => new("Completed", 3);
        public static TaskStatus Default => Todo;

        private TaskStatus(string value, byte number)
        {

            AgainstNullOrWhiteSpace(value);
            AgainstInSupportedStatusNumber(number);
            Value = value.Trim();
            Number = number;
        }

        private void AgainstNullOrWhiteSpace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("TaskStatus value required");
        }
        private void AgainstInSupportedStatusNumber(byte number)
        {

            if (number < 1 || number > 3)
                throw new ArgumentException("TaskStatus number must be (ie.1..3)");
        }

        public TaskStatus ReDo()
        {
            if (this == InProgress && this != Completed)
                return Todo;
            return this;
        }

        public TaskStatus SetInProgress()
        {
            if (this != InProgress && this != Completed)
                return InProgress;
            return this;
        }

        public TaskStatus Complete()
        {
            if (this == Todo && this != InProgress)
                return Completed;
            return this;
        }

        public static bool operator ==(TaskStatus a, TaskStatus b) =>
            (ReferenceEquals(a, b) || 
            (a is not null && b is not null) 
            && a?.Number == b?.Number);

        public static bool operator !=(TaskStatus a, TaskStatus b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) => 
            obj is TaskStatus status && Number == status.Number;

        public override int GetHashCode() => HashCode.Combine(Value, Number);
    }
}
