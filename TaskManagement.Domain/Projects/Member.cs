
using TaskManagement.Domain.Projects.ObjectValues;
using TaskManagement.Domain.Tasks.Value_Objects;
using TaskManagement.Domain.Users.ObjectValues;

namespace TaskManagement.Domain.Projects
{
    public sealed class Member
    {
        public UserId UserId { get; private set; }
        public MemberRole Role { get; private set; } = MemberRole.Viewer;
        public DateTime JoinedAtUtc { get; private set; }
        public bool IsActive { get; private set; }
        //TaskIds assigned to the member in the project
        private List<TaskId> TaskIds = new();
        public IReadOnlyCollection<TaskId> AssignedTaskIds => TaskIds.AsReadOnly();


        private Member
            (
                UserId userId,
                MemberRole role,
                DateTime joinedAtUtc, 
                bool isActive, 
                List<TaskId> taskIds
            )
        {
            UserId = userId;
            Role = role;
            JoinedAtUtc = joinedAtUtc;
            IsActive = isActive;
            TaskIds = taskIds;
        }

        public static Member Create
            (
                UserId userId,
                MemberRole role,
                DateTime joinedAtUtc
            )
        {
            
            return new Member
                (
                    userId,
                    role,
                    joinedAtUtc,
                    true,
                    new List<TaskId>()
                );
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new Exception("Member is already deactivated");
            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive)
                throw new Exception("Member is already active");
            IsActive = true;
        }
        public void ChangeRole(MemberRole newRole)
        {
            if(newRole==Role)
                throw new Exception("Member already has the role");
            if(newRole == MemberRole.Owner)
                throw new Exception("Cannot assign Owner role to a member");
            Role = newRole;

        }
        public void AssignTask(TaskId taskId)
        {
            if(TaskIds.Contains (taskId))
                throw new Exception("Task already assigned to the member");
            TaskIds.Add(taskId);
        }
        public void UnAssignTask(TaskId taskId)
        {
            if (!TaskIds.Contains(taskId))
                throw new Exception("Task not assigned to the member");
            TaskIds.Remove(taskId);
        }
        public int AssignedTaskCount() => TaskIds.Count;

        public override string ToString() => 
            $"{UserId}-{Role}-{(IsActive ? "Active" : "Inactive")}";
    }
}
