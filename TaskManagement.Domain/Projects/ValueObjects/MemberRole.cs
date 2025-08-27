using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.ValueObjects           
{
    public sealed class MemberRole
    {
        public string Value { get; private set; }
        public byte Number { get; private set; }
        public string Display => Value;
        public static MemberRole Owner => new("Owner", 1);
        public static MemberRole Admin => new("Admin", 2);
        public static MemberRole Member => new("Member", 3);
        public static MemberRole Viewer => new("Viewer", 4);
        public static MemberRole Default => Viewer;

        private MemberRole(string value, byte number)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("MemberRole value required");
            if (number < 1 || number > 4)
                throw new ArgumentException("MemberRole number must be (ie.1..4)");
            Value = value.Trim();
            Number = number;
        }
        public static bool operator ==(MemberRole a, MemberRole b) =>
            (ReferenceEquals(a, b) || (a is not null && b is not null) && a?.Number == b?.Number);

        public static bool operator !=(MemberRole a, MemberRole b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) =>
            obj is MemberRole role && this == role;

        public override int GetHashCode() => HashCode.Combine(Number,Value);

       
    }
}
