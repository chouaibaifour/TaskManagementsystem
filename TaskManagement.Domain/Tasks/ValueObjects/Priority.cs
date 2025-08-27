using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Tasks.ValueObjects
{
   public sealed class Priority
    {
        public string Value { get; private set; }
        public byte Number { get; private set; }
        public string Display => Value;

        public static Priority Low => new("Low", 1);
        public static Priority Meduim => new("Meduim", 2);
        public static Priority High => new("High", 3);
        public static Priority Default => Low;

        private Priority(string value, byte number)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Priority value required");

            if (number < 1 || number > 3)
                throw new ArgumentException("Priority number must be (ie.1..3)");

            Value = value.Trim();
            Number = number;
        }

        

        public static bool operator ==(Priority a, Priority b) =>
            ReferenceEquals(a, b) ||
            a is not null && b is not null
            && a?.Number == b?.Number;

        public static bool operator !=(Priority a, Priority b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) =>
            obj is Priority status && Number == status.Number;

        public override int GetHashCode() => HashCode.Combine(Value, Number);
    }
}
