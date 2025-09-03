
namespace TaskManagement.Domain.Users.ValueObjects
{
    public sealed class UserRole
    {
        private bool Equals(UserRole other)
        {
            return _value == other._value;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is UserRole other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)_value;
        }

        private enum EnUserRole{
                Member = 0,
                Manager =1,
                Admin = 2
                
            }

            private readonly EnUserRole _value;
            
            private UserRole(EnUserRole value)=> _value=value;
            public static UserRole Member =>new (EnUserRole.Member);
            public static UserRole Manager=>new (EnUserRole.Manager);
            public static UserRole Admin=>new (EnUserRole.Admin);
            public static UserRole Default=>Member;
            
            public static bool operator ==(UserRole x, UserRole y)
            {
                return (ReferenceEquals(x, y) || x._value == y._value);
            }

            public static bool operator !=(UserRole x, UserRole y)
            {
                return !(x == y);
            }

            public static UserRole FromEnum(Enum value)
            {
                Enum.TryParse<EnUserRole>(value.ToString(), out var role);
                return new UserRole(role);
            }

            public Enum ToEnum()
            {
                return _value;
            }
    }
   
}
