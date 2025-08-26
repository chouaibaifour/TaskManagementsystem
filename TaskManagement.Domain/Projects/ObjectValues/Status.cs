using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.ObjectValues
{
    public sealed class Status
    {
        public string Value { get; private set; }
        public byte Number { get; private set; }
        public string Display => Value;

        public static Status Active =>new ("Active",1);
        public static Status Archived => new("Archived",2);
        public static Status Completed => new("Completed", 3);
        public static Status Default => Active;

        private Status(string value, byte number)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Status value required");

            if (number < 1 || number > 3) 
                throw new ArgumentException("Status number must be (ie.1..3)");

            Value = value.Trim();
            Number = number;
        }

        

        public Status ReActivte()
        {
            if (this == Archived && this != Completed)
                return Active;
            return this;
        }

        public Status Archive()
        {
            if (this!=Archived && this != Completed)
                return Archived;
            return this;
        }

        public Status Complete()
        {
            if (this == Active && this != Archived)
                return Completed;
            return this;
        }

        public static bool operator ==(Status a, Status b) =>
            (ReferenceEquals(a, b) || (a is not null && b is not null) && a?.Number == b?.Number);

        public static bool operator !=(Status a, Status b) =>!( a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) => obj is Status status && Number == status.Number;

        public override int GetHashCode() => HashCode.Combine(Value, Number);

    }
}
