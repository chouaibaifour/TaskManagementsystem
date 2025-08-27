using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.ValueObjects
{
    public sealed class Email
    {
        private static readonly Regex Pattren = new(
            "^(?=.{5,254}$)([a-zA-Z0-9_\\'+\\-.]+)@([a-zA-Z0-9\\-.]+)\\\\.([a-zA-Z]{2,})$"
            , RegexOptions.Compiled | RegexOptions.CultureInvariant);

       public string Value { get; }

        private Email(string value) => Value = value;

        public static Email Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value))throw new ArgumentException("Email is required");
            value = value.Trim();
            if(!Pattren.IsMatch(value)) throw new ArgumentException("Invalid Email format");
            return new Email(value.ToLowerInvariant());

        }
        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(Email left, Email right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Value.Equals(right.Value, StringComparison.OrdinalIgnoreCase);
        }
        public static bool operator !=(Email left, Email right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj is Email email && this == email;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
