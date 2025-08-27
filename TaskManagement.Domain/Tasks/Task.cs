
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.Events;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks
{
    public class Task:AggregateRoot<TaskId>
    {
       
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public Status Status { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime DueDate { get; private set; }
        public UserId CreatedById { get; private set; }
        public UserId AssignedToId { get; private set; }
        public ProjectId ProjectId { get; private set; }
        private List<Comment> _Comments= new List<Comment>();
        public IReadOnlyCollection<Comment> Comments =>_Comments.AsReadOnly();
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdatedAtUtc { get; private set; }

        private Task(
            TaskId taskId,
            Title title,
            Description description,
            ProjectId projectId,
            UserId createdById,
            Priority priority,
            DateTime dueDate,
            Status status,
            DateTime createdAtUtc)
        {
            
            if (dueDate < createdAtUtc)
                throw new ArgumentException("Due date cannot be in the past.");

            Id = taskId;
            Title = title;
            Description = description;
            ProjectId = projectId;
            CreatedById = createdById;
            Priority = priority;
            Status = status;
            DueDate = dueDate;
            CreatedAtUtc = createdAtUtc;
        }

        public static Task Create(
            Title title,
            Description description,
            ProjectId projectId,
            UserId createdById,
            UserId? assignedToId,
            Priority priority,
            DateTime dueDate,
            IClock clock) // Dependency for time
        {
           if (dueDate < clock.UtcNow)
               throw new ArgumentException("Due date cannot be in the past.");

            var task = new Task
                (
                    TaskId.New(),
                    title,
                    description,
                    projectId,
                    createdById,
                    priority,
                    dueDate,
                    Status.Default,
                    clock.UtcNow
                );
            task.Raise(new TaskCreatedEvent(task.Id, task.ProjectId, task.CreatedById));

            return task;
        }
        private void Touch(IClock clock) => UpdatedAtUtc = clock.UtcNow;

        
        private  void UpdateStatus(Status newStatus,IClock clock)
        {
            if(!Status.CanTransitionTo(newStatus))
                throw new InvalidOperationException(
                    $"can not transit Status from {Status.Display} to {newStatus.Display}."
                    );
            var oldStatus = Status;
            Status = newStatus;
            Touch(clock);
            Raise(new TaskStatusChangedEvent(Id, oldStatus, newStatus));
        }

        public void Start(IClock clock) => UpdateStatus(Status.InProgress,  clock);
        public void Complete(IClock clock) => UpdateStatus(Status.Completed, clock);
        public void ReOpen(IClock clock) => UpdateStatus(Status.Todo, clock);


    
    }
}
