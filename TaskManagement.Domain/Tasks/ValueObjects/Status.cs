using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;

namespace TaskManagement.Domain.Tasks.ValueObjects
{
    public sealed class Status
    {
        public string Value { get; private set; }
        public byte Number { get; private set; }
        public string Display => Value;

        public static Status Todo => new("To do", 1);
        public static Status InProgress => new("InProgress", 2);
        public static Status Completed => new("Completed", 3);
        public static Status Default => Todo;

        private Status(string value, byte number)
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

        

        public static bool operator ==(Status a, Status b) =>
            (ReferenceEquals(a, b) || 
            (a is not null && b is not null) 
            && a?.Number == b?.Number);

        public static bool operator !=(Status a, Status b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) => 
            obj is Status status && Number == status.Number;

        public override int GetHashCode() => HashCode.Combine(Value, Number);

        internal bool CanTransitionTo(Status newStatus)
        {
            if (this == newStatus)
                return true;
            if (this == Todo&&newStatus==InProgress)
                return true;
            if(this==InProgress&&newStatus==Completed)
                return true;
            if (this==Completed&&newStatus==Todo)
                return true;
            return false;

        }
    }
}
