
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
using TaskStatus = TaskManagement.Domain.Tasks.ValueObjects.TaskStatus;

namespace TaskManagement.Domain.Tasks
{
    public class Task : AggregateRoot<TaskId>
    {

        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public TaskStatus TaskStatus { get; private set; }
        public TaskPriority TaskPriority { get; private set; }
        public DateTime DueDate { get; private set; }
        public UserId CreatedById { get; private set; }
        public UserId AssignedToId { get; private set; }
        public ProjectId ProjectId { get; private set; }
        private readonly List<Comment> _comments;
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdatedAtUtc { get; private set; }

        private Task(
            TaskId taskId,
            Title title,
            Description description,
            ProjectId projectId,
            UserId createdById,
            UserId assignedToId,
            TaskPriority taskPriority,
            DateTime dueDate,
            TaskStatus taskStatus,
            DateTime createdAtUtc,
            List<Comment> comments)
        {

            if (dueDate < createdAtUtc)
                throw new ArgumentException("Due date cannot be in the past.");

            Id = taskId;
            Title = title;
            Description = description;
            ProjectId = projectId;
            CreatedById = createdById;
            AssignedToId = assignedToId;
            TaskPriority = taskPriority;
            TaskStatus = taskStatus;
            DueDate = dueDate;
            CreatedAtUtc = createdAtUtc;
            _comments = comments;
        }
        
        private Task(
            TaskId taskId,
            Title title,
            Description description,
            ProjectId projectId,
            UserId createdById,
            
            TaskPriority taskPriority,
            DateTime dueDate,
            TaskStatus taskStatus,
            DateTime createdAtUtc,
            List<Comment> comments)
        {

            if (dueDate < createdAtUtc)
                throw new ArgumentException("Due date cannot be in the past.");

            Id = taskId;
            Title = title;
            Description = description;
            ProjectId = projectId;
            CreatedById = createdById;
            
            TaskPriority = taskPriority;
            TaskStatus = taskStatus;
            DueDate = dueDate;
            CreatedAtUtc = createdAtUtc;
            _comments = comments;
        }
        

        public static Task Create(
            Title title,
            Description description,
            ProjectId projectId,
            UserId createdById,
            UserId? assignedToId,
            TaskPriority taskPriority,
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
                    taskPriority,
                    dueDate,
                    TaskStatus.Default,
                    clock.UtcNow,
                    []
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

        private void UpdatePriority(TaskPriority newTaskPriority, IClock clock)
        {
            if (newTaskPriority == TaskPriority)
                throw new InvalidOperationException("TaskPriority is already set to this value.");
            var oldPriority = TaskPriority;
            TaskPriority = newTaskPriority;
            Touch(clock);
            Raise(new TaskPriorityChangedEvent(Id, oldPriority, newTaskPriority,  clock.UtcNow));
        }
        public void LowPriority(IClock clock) => UpdatePriority(TaskPriority.Low, clock);
        public void MediumPriority(IClock clock) => UpdatePriority(TaskPriority.Medium, clock);
        public void HighPriority(IClock clock) => UpdatePriority(TaskPriority.High, clock);

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

        private void UpdateStatus(TaskStatus newTaskStatus, IClock clock)
        {
            if (!TaskStatus.CanTransitionTo(newTaskStatus))
                throw new InvalidOperationException(
                    $"can not transit TaskStatus from {TaskStatus.Display} to {newTaskStatus.Display}."
                    );
            var oldStatus = TaskStatus;
            TaskStatus = newTaskStatus;
            Touch(clock);
            Raise(new TaskStatusChangedEvent(Id, oldStatus, newTaskStatus));
        }

        public void Start(IClock clock) => UpdateStatus(TaskStatus.InProgress, clock);
        public void Complete(IClock clock) => UpdateStatus(TaskStatus.Completed, clock);
        public void ReOpen(IClock clock) => UpdateStatus(TaskStatus.Todo, clock);

        public void AssignTo(UserId newAssigneeId, IClock clock)
        {
            if (newAssigneeId == AssignedToId)
                throw new InvalidOperationException("Task is already assigned to this user.");
            var oldAssigneeId = AssignedToId;
            AssignedToId = newAssigneeId;
            Touch(clock);
            Raise(new TaskAssignedEvent(Id, oldAssigneeId, newAssigneeId, clock.UtcNow));
        }

        public Comment AddComment(CommentId commentId, UserId authorId, CommentContent content, IClock clock)
        {
            var comment = Comment.Create(commentId, authorId, content, clock.UtcNow);
            _comments.Add(comment);
            Touch(clock);
            Raise(new CommentAddedEvent(Id, authorId, comment.CommentId, clock.UtcNow));
            return comment;
        }

        public Comment EditComment(CommentId commentId, CommentContent newContent, UserId editorId, IClock clock)
        {
            var comment = _comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new InvalidOperationException("Comment not found.");
            if (comment.AuthorId != editorId)
                throw new InvalidOperationException("Only the author can edit this comment.");
            comment.Update(newContent,clock);
            Touch(clock);
            Raise(new CommentEditedEvent(Id,  commentId,editorId, clock.UtcNow));
            return comment;
        }

        public void DeleteComment(CommentId commentId, UserId deleterId, IClock clock)
        {
            var comment = _comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new InvalidOperationException("Comment not found.");
            if (comment.AuthorId != deleterId)
                throw new InvalidOperationException("Only the author can delete this comment.");
            _comments.Remove(comment);
            Touch(clock);
            Raise(new CommentDeletedEvent(Id, commentId, deleterId, clock.UtcNow));
        }

        public Comment GetComment(CommentId commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new InvalidOperationException("Comment not found.");
            return comment;
        }

        public static Task Rehydrate
        (
            TaskId taskId,
            Title title,
            Description description,
            TaskStatus status,
            TaskPriority priority,
            DateTime dueDate,
            UserId createdById,
            UserId assignedToId,
            ProjectId projectId,
            DateTime createdAtUtc,
            DateTime? updateAtUtc,
            List<Comment> comments

        )
            => new
            ( 
                taskId, 
                title, 
                description, 
                projectId, 
                createdById, 
                assignedToId,
                priority, 
                dueDate, 
                status, 
                createdAtUtc,
                comments


            );

    }
}
