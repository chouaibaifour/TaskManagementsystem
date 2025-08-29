
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.Events;
using TaskManagement.Domain.Tasks.Events.Comment;
using TaskManagement.Domain.Tasks.Events.NewFolder;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks
{
    public class Task : AggregateRoot<TaskId>
    {

        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public Status Status { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime DueDate { get; private set; }
        public UserId CreatedById { get; private set; }
        public UserId AssignedToId { get; private set; }
        public ProjectId ProjectId { get; private set; }
        private List<Comment> _Comments = new List<Comment>();
        public IReadOnlyCollection<Comment> Comments => _Comments.AsReadOnly();
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

        public void UpdateDetails
        (
            Title newTitle,
            Description newDescription, 
            UserId updatedById,
            IClock clock
        )
        {
            
            Title = newTitle;
            Description = newDescription;
            

            Touch(clock);

            Raise(new TaskDetailsUpdatedEvent(Id, updatedById, clock.UtcNow));

        }

        private void UpdatePriority(Priority newPriority, IClock clock)
        {
            if (newPriority == Priority)
                throw new InvalidOperationException("Priority is already set to this value.");
            var oldPriority = Priority;
            Priority = newPriority;
            Touch(clock);
            Raise(new TaskPriorityChangedEvent(Id, oldPriority, newPriority,  clock.UtcNow));
        }
        public void LowPriority(IClock clock) => UpdatePriority(Priority.Low, clock);
        public void MediumPriority(IClock clock) => UpdatePriority(Priority.Meduim, clock);
        public void HighPriority(IClock clock) => UpdatePriority(Priority.High, clock);

        public void UpdateDueDate(DateTime newDueDate, IClock clock)
        {
            if (newDueDate < clock.UtcNow)
                throw new ArgumentException("Due date cannot be in the past.");
            if (newDueDate == DueDate)
                throw new InvalidOperationException("Due date is already set to this value.");
            var oldDueDate = DueDate;
            DueDate = newDueDate;
            Touch(clock);
            Raise(new TaskDueDateChangedEvent(Id, oldDueDate, newDueDate, clock.UtcNow));
        }

        private void UpdateStatus(Status newStatus, IClock clock)
        {
            if (!Status.CanTransitionTo(newStatus))
                throw new InvalidOperationException(
                    $"can not transit Status from {Status.Display} to {newStatus.Display}."
                    );
            var oldStatus = Status;
            Status = newStatus;
            Touch(clock);
            Raise(new TaskStatusChangedEvent(Id, oldStatus, newStatus));
        }

        public void Start(IClock clock) => UpdateStatus(Status.InProgress, clock);
        public void Complete(IClock clock) => UpdateStatus(Status.Completed, clock);
        public void ReOpen(IClock clock) => UpdateStatus(Status.Todo, clock);

        public void AssignTo(UserId newAssigneeId, IClock clock)
        {
            if (newAssigneeId == AssignedToId)
                throw new InvalidOperationException("Task is already assigned to this user.");
            var oldAssigneeId = AssignedToId;
            AssignedToId = newAssigneeId;
            Touch(clock);
            Raise(new TaskAssignedEvent(Id, oldAssigneeId, newAssigneeId, clock.UtcNow));
        }

        public void AddComment(CommentId commentId, UserId authorId, CommentContent content, IClock clock)
        {
            var comment = Comment.Create(commentId, authorId, content, clock.UtcNow);
            _Comments.Add(comment);
            Touch(clock);
            Raise(new CommentAddedEvent(Id, authorId, comment.CommentId, clock.UtcNow));
        }

        public void EditComment(CommentId commentId, CommentContent newContent, UserId editorId, IClock clock)
        {
            var comment = _Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new InvalidOperationException("Comment not found.");
            if (comment.AuthorId != editorId)
                throw new InvalidOperationException("Only the author can edit this comment.");
            comment.Update(newContent);
            Touch(clock);
            Raise(new CommentEditedEvent(Id,  commentId,editorId, clock.UtcNow));
        }

        public void DeleteComment(CommentId commentId, UserId deleterId, IClock clock)
        {
            var comment = _Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new InvalidOperationException("Comment not found.");
            if (comment.AuthorId != deleterId)
                throw new InvalidOperationException("Only the author can delete this comment.");
            _Comments.Remove(comment);
            Touch(clock);
            Raise(new CommentDeletedEvent(Id, commentId, deleterId, clock.UtcNow));
        }
         
    }
}
