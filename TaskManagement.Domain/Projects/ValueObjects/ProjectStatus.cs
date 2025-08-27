using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.ValueObjects
{
    public sealed class ProjectStatus
    {
        public string Value { get; private set; }
        public byte Number { get; private set; }
        public string Display => Value;

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

            Value = value.Trim();
            Number = number;
        }

        

        public ProjectStatus ReActivte()
        {
            if (this == Archived && this != Completed)
                return Active;
            return this;
        }

        public ProjectStatus Archive()
        {
            if (this!=Archived && this != Completed)
                return Archived;
            return this;
        }

        public ProjectStatus Complete()
        {
            if (this == Active && this != Archived)
                return Completed;
            return this;
        }

        public static bool operator ==(ProjectStatus a, ProjectStatus b) =>
            (ReferenceEquals(a, b) || (a is not null && b is not null) && a?.Number == b?.Number);

        public static bool operator !=(ProjectStatus a, ProjectStatus b) =>!( a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) => obj is ProjectStatus status && Number == status.Number;

        public override int GetHashCode() => HashCode.Combine(Value, Number);

        internal bool CanTransitionTo(ProjectStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
