

using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Projects
{
    public sealed class Member
    {
        public UserId UserId { get; private set; }
        public MemberRole Role { get; private set; }
        public DateTime JoinedAtUtc { get; private set; }
        public bool IsActive { get; private set; }
        //TaskIds assigned to the member in the project
        private readonly List<TaskId> _taskIds;
        public IReadOnlyCollection<TaskId> AssignedTaskIds => _taskIds.AsReadOnly();


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
            _taskIds = taskIds;
        }

        public static Member Rehydrate
        (
            UserId userId,
            MemberRole role,
            DateTime joinedAtUtc, 
            bool isActive, 
            List<TaskId> taskIds
            )
        =>
            new(userId,role,joinedAtUtc,isActive,taskIds);
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
                throw new Exception("Member already has the userRole");
            if(newRole == MemberRole.Owner)
                throw new Exception("Cannot assign Owner userRole to a member");
            Role = newRole;

        }
        public void AssignTask(TaskId taskId)
        {
            if(_taskIds.Contains (taskId))
                throw new Exception("Task already assigned to the member");
            _taskIds.Add(taskId);
        }
        public void UnAssignTask(TaskId taskId)
        {
            if (!_taskIds.Contains(taskId))
                throw new Exception("Task not assigned to the member");
            _taskIds.Remove(taskId);
        }
        public int AssignedTaskCount() => _taskIds.Count;

        public override string ToString() => 
            $"{UserId}-{Role}-{(IsActive ? "Active" : "Inactive")}";
    }
}
