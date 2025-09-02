

namespace TaskManagement.Domain.Common.Primitives.ValueObject
{
    public abstract class Content(string value, string fieldName)
    {
        private readonly string _value = value.Trim();

        private  string Display => _value;


        protected void EnsureMaxLength(string value ,int maxLength)
        {
            if (value.Length > maxLength)
                throw new ArgumentException($"{fieldName} cannot exceed {maxLength} characters.");
        }

        protected void EnsureRequired(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException($"{fieldName} cannot be null or whiteSpace.");
        }

        public override string ToString() => Display;

        public static bool operator ==(Content a, Content b)
        {
            if (a.Equals( b))
                return true;
                       
            return a._value == b._value;
        }
        public static bool operator !=(Content a, Content b) => !(a == b);

        public override bool Equals(object? obj)
        {
            return (ReferenceEquals(this, obj) && !ReferenceEquals(obj, null));

        }

        public override int GetHashCode()
        {
            return 1;
        }

    }
}
