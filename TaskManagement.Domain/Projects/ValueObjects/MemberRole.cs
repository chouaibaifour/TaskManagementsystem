
namespace TaskManagement.Domain.Projects.ValueObjects           
{
    public sealed class MemberRole
    {
        

        private enum EnMemberRole
        {
            Owner,
            Member,
            Admin,
            Viewer
        }
        
        private readonly EnMemberRole _value;
        
        public string Display => _value.ToString();
        public static MemberRole Owner => new(EnMemberRole.Owner);
        public static MemberRole Admin => new(EnMemberRole.Admin);
        public static MemberRole Member => new(EnMemberRole.Member);
        public static MemberRole Viewer => new(EnMemberRole.Viewer);
        public static MemberRole Default => Viewer;

        private MemberRole(EnMemberRole role)
        {
           _value = role;
            
        }
        public static bool operator ==(MemberRole a, MemberRole b) =>
            (ReferenceEquals(a, b) ||  a._value == b._value);

        public static bool operator !=(MemberRole a, MemberRole b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) =>
            obj is MemberRole role && this == role;

        public override int GetHashCode() => HashCode.Combine(_value,_value);

        public static MemberRole FromEnum(Enum value)
        {
            Enum.TryParse<EnMemberRole>(value.ToString(), out var role  );
            return new MemberRole(role);
        }
        public Enum ToEnum()=>_value;
       
    }
}
