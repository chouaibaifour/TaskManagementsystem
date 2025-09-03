namespace TaskManagement.Domain.Tasks.ValueObjects
{
   public sealed class TaskPriority
   {
       private enum EnPriority 
       {
           Low,
           Medium,
           High
       }

       private readonly EnPriority _value;
        
        public string Display => _value.ToString();

        public static TaskPriority Low => new(EnPriority.Low);
        public static TaskPriority Medium => new(EnPriority.Medium);
        public static TaskPriority High => new(EnPriority.High);
        public static TaskPriority Default => Low;
        private TaskPriority(EnPriority value)=>_value = value;

        public static bool operator ==(TaskPriority a, TaskPriority b) =>
            ReferenceEquals(a, b) ||
             a._value == b._value;

        public static bool operator !=(TaskPriority a, TaskPriority b) => !(a == b);

        public override string ToString() => Display;

        public override bool Equals(object? obj) =>
            obj is TaskPriority status && _value == status._value;

        public override int GetHashCode() => HashCode.Combine(_value);

        public Enum ToEnum() => _value;

        public static TaskPriority FromEnum(Enum value)
        {
            Enum.TryParse<EnPriority>(value.ToString(), out var enPriority);
            return new TaskPriority(enPriority);
        }



    }
}
