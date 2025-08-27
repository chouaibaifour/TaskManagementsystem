
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.Events;
using TaskManagement.Domain.Projects.Events.ProjectMember;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Projects
{
    public class Project:AggregateRoot<ProjectId>
    {
        public ProjectName Name { get; private set; }
        public Description Description { get; private set; }
        public UserId OwnerId { get; private set; }
        public ProjectStatus Status { get; private set; } = ProjectStatus.Default;
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdateAtUtc { get; private set; }
        private List<Member> _members  = new List<Member>(13);

        public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

        private Project(
            ProjectId id,
            ProjectName name,
            Description description,
            UserId ownerId,
            ProjectStatus status,
            DateTime createdAtUtc,
            List<Member> members)
        {
            Id = id;
            Name = name;
            Description = description;
            OwnerId = ownerId;
            Status = status;
            CreatedAtUtc = createdAtUtc;
           
            _members = members;
        }

        public static Project Create
        (
            ProjectName name,
            Description description,
            UserId ownerId,
            IClock clock)
        {

            var nowUtc = clock.UtcNow;

            var project= new Project(
                ProjectId.New(),
                name,
                description,
                ownerId,
                ProjectStatus.Default,
                nowUtc,
                
                new List<Member>
                {
                    Member.Create(ownerId,MemberRole.Owner,nowUtc)
                });

            project.Raise(new ProjectCreatedEvent(project.Id, project.OwnerId, nowUtc));

            return project;
        }

        private void Touch(IClock clock) =>
            UpdateAtUtc = clock.UtcNow;

        public void UpdateDetails(ProjectName name, Description description,IClock clock)
        {
            Name = name;
            Description = description;
            Touch(clock);
        }

        
        public void Complete(IClock clock) => updateProjectStatus(ProjectStatus.Completed,clock);
        public void Archive(IClock clock) => updateProjectStatus(ProjectStatus.Archived, clock);
        public void ReActivate(IClock clock) => updateProjectStatus(ProjectStatus.Completed,clock);
        
        private void updateProjectStatus(ProjectStatus newStatus,IClock clock)
        {
            if (!Status.CanTransitionTo(newStatus))
                throw new InvalidOperationException(
                    $"can not transit Status from {Status.Display} to {newStatus.Display}."
                    );
            var oldStatus = Status;
            Status = newStatus;
            Touch(clock);
            //Raise(new ProjectChangeStatusEvent());
        }
        private void AddMemberInternal(UserId userId,MemberRole role,IClock clock)
        {
            if(role== MemberRole.Owner)
                throw new Exception("Cannot add another Owner to the project");

            if (Members.Any(m => m.UserId == userId))
                throw new Exception("User is already a member of the project");

            if (Members.Count >= 13) 
                throw new Exception("Project has reached the maximum number of members (13)");


            _members.Add(Member.Create(userId, role, clock.UtcNow));

            Touch(clock);
            Raise(new ProjectMemberAddedEvent(Id, userId, role, clock.UtcNow));
        }

        public void AddMember(UserId userId, IClock clock)=>
                AddMemberInternal(userId, MemberRole.Viewer, clock);

        public void AddAdmin(UserId userId, IClock clock)=>
                AddMemberInternal(userId, MemberRole.Admin, clock);

        public void AddViewer(UserId userId, IClock clock) =>
                AddMemberInternal(userId, MemberRole.Viewer, clock);

        public void RemoveMember(UserId userId,IClock clock
            )
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);
            if (member == null)
                throw new Exception("User is not an active member of the project");

            if (member.Role == MemberRole.Owner)
                throw new Exception("Cannot remove the Owner from the project");

            if(!_members.Remove(member)) throw new Exception("Failed to remove member from the project");

            Touch(clock);
            Raise(new ProjectMemberRemovedEvent(Id, userId, clock.UtcNow));
        }
        
       
        public void DeactivateMember(UserId userId)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);
            if (member == null)
                throw new Exception("User is not an active member of the project");
            if (member.Role == MemberRole.Owner)
                throw new Exception("Cannot remove the Owner from the project");
            member.Deactivate();
        }
        public void ActivateMember(UserId userId)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && !m.IsActive);
            if (member == null)
                throw new Exception("User is not a deactivated member of the project");
            member.Activate();
        }
         
        public void ChangeMemberRole(UserId userId, MemberRole role)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);
            if (member == null)
                throw new Exception("User is not an active member of the project");
            if (member.Role == MemberRole.Owner)
                throw new Exception("Cannot change the role of the Owner");
            member.ChangeRole(role);
            Raise(new ProjectMemberRoleChangedEvent(Id, userId, role, DateTime.UtcNow));
        }
         public void AssignTaskToMember(UserId userId, TaskId taskId,IClock clock)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);

            if (member == null)
                throw new Exception("User is not an active member of the project");

            member.AssignTask(taskId);

            Raise(new ProjectMemberTaskAssignedEvent(Id, userId, taskId, clock.UtcNow));

        }
        public void UnAssignTaskFromMember(UserId userId, TaskId taskId)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);
            if (member == null)
                throw new Exception("User is not an active member of the project");
            member.UnAssignTask(taskId);
        }

    }
}
