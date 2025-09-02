
namespace TaskManagement.Domain.Projects.ValueObjects
{
    public sealed class ProjectStatus
    {
        private readonly string _value;
        private readonly byte _number;
        private string Display => _value;

        public static ProjectStatus Active =>new ("Active",1);
        public static ProjectStatus Archived => new("Archived",2);
        public static ProjectStatus Completed => new("Completed", 3);
        public static ProjectStatus Default => Active;

        private ProjectStatus(string value, byte number)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ProjectStatus value required");

            if (number < 1 || number > 3) 
                throw new ArgumentException("ProjectStatus number must be (ie.1..3)");

            _value = value.Trim();
            _number = number;
        }

        public static bool operator ==(ProjectStatus a, ProjectStatus b) =>
            (ReferenceEquals(a, b) && a._number == b._number);

        public static bool operator !=(ProjectStatus a, ProjectStatus b) =>!( a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) => obj is ProjectStatus status && _number == status._number;

        public override int GetHashCode() => HashCode.Combine(_value, _number);

        internal bool CanTransitionTo(ProjectStatus newStatus)
        {
            if(this == newStatus)
                return false;

            if (CanTransitionToArchived() || 
                CanTransitionToCompleted() || 
                CanTransitionToActive())
                return true;

            return false;
        }

        private bool CanTransitionToActive()
        {
            return (this == Archived);
        }

        private bool CanTransitionToCompleted()
        {
            return (this == Active);
        }

        private bool CanTransitionToArchived()
        {
                return (this == Active);
        }
    }
}
