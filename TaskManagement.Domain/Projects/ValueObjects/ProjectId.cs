


namespace TaskManagement.Domain.Projects.ValueObjects
{
    public readonly record struct ProjectId(Guid Value)
    {
        public static ProjectId New() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();

        public static implicit operator Guid(ProjectId ProjectId) => ProjectId.Value;

        public static implicit operator ProjectId(Guid Value) => new(Value);

    }
}
