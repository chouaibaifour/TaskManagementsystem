
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Projects.Events;
using TaskManagement.Domain.Projects.ObjectValues;
using TaskManagement.Domain.Tasks.Value_Objects;
using TaskManagement.Domain.Users.ObjectValues;

namespace TaskManagement.Domain.Projects
{
    public class Project:AggregateRoot<ProjectId>
    {
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public UserId OwnerId { get; private set; }
        public Status Status { get; private set; } = Status.Default;
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdateAtUtc { get; private set; }
        public List<Member> _members { get; private set; } = new List<Member>(13);

        public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

        private Project(
            ProjectId id,
            Name name,
            Description description,
            UserId ownerId,
            Status status,
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
            Name name,
            Description description,
            UserId ownerId,
            DateTime nowUtc)
        {

            return new Project(
                ProjectId.New(),
                name,
                description,
                ownerId,
                Status.Default,
                nowUtc,
                
                new List<Member>
                {
                    Member.Create(ownerId,MemberRole.Owner,nowUtc)
                });
        }

        private void Touch(DateTime nowUtc) =>
            UpdateAtUtc = nowUtc;

        public void UpdateDetails(Name name, Description description,DateTime nowUtc)
        {
            Name = name;
            Description = description;
            Touch(nowUtc);
        }

        public void ReActivate(DateTime nowUtc)
        {
          
            if (Status == Status.Active)
                return;

            Status = Status.ReActivte();
            Touch(nowUtc);

            Raise(new ProjectReActivateEvent(Id, nowUtc));
        }
        public void Archive(DateTime nowUtc)
        {
            
            if (Status.Archived == Status)
                return;
            Status = Status.Archive();
            Touch(nowUtc);

            Raise(new ProjectArchivedEvent(Id, nowUtc));
        }
        public void Complete(DateTime nowUtc)
        {
            
            if (Status.Completed == Status)
                return;
            Status = Status.Complete();
            Touch(nowUtc);
            Raise(new ProjectCompletedEvent(Id, nowUtc));
        }
        private void AddMemberInternal(UserId userId,MemberRole role,DateTime nowUtc)
        {
            if(role== MemberRole.Owner)
                throw new Exception("Cannot add another Owner to the project");

            if (Members.Any(m => m.UserId == userId))
                throw new Exception("User is already a member of the project");

            if (Members.Count >= 13) 
                throw new Exception("Project has reached the maximum number of members (13)");


            _members.Add(Member.Create(userId, role, nowUtc));

            Touch(nowUtc);
            Raise(new ProjectMemberAddedEvent(Id, userId, role, nowUtc));
        }

        public void AddMember(UserId userId, DateTime nowUtc)=>
                AddMemberInternal(userId, MemberRole.Viewer, nowUtc);

        public void AddAdmin(UserId userId, DateTime nowUtc)=>
                AddMemberInternal(userId, MemberRole.Admin, nowUtc);

        public void AddViewer(UserId userId, DateTime nowUtc) =>
                AddMemberInternal(userId, MemberRole.Viewer, nowUtc);

        public void RemoveMember(UserId userId,DateTime nowUtc
            )
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);
            if (member == null)
                throw new Exception("User is not an active member of the project");

            if (member.Role == MemberRole.Owner)
                throw new Exception("Cannot remove the Owner from the project");

            if(!_members.Remove(member)) throw new Exception("Failed to remove member from the project");

            Touch(nowUtc);
            Raise(new ProjectMemberRemovedEvent(Id, userId, nowUtc));
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
         public void AssignTaskToMember(UserId userId, TaskId taskId)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId && m.IsActive);

            if (member == null)
                throw new Exception("User is not an active member of the project");

            member.AssignTask(taskId);

            Raise(new ProjectMemberTaskAssignedEvent(Id, userId, taskId, DateTime.UtcNow));

        }

    }
}
