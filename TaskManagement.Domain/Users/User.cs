
using System.Data;
using System.Xml.Linq;
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Users.Enums;


using TaskManagement.Domain.Users.Events;
using TaskManagement.Domain.Users.Policies;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Users
{
    public class User : AggregateRoot<UserId>
    {
        public FullName Name { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Role Role { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }
      
        public NotificationSettings NotificationSettings { get; private set; }
        public DateTime? LastLoginAtUtc { get; private set; }

        private User(
            UserId id,
            FullName name,
            Email email,
            PasswordHash passwordHash,
            Role role,
            DateTime createdAtUtc,
            NotificationSettings notificationSettings)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAtUtc = createdAtUtc;
            NotificationSettings = notificationSettings;
        }

        
        public static User RegisterAsync
        (
            FullName name,
            Email email,
            PasswordHash passwordHash,
            Role role,
            IClock clock
           
        )
        {

            var user = new User
                (
                    UserId.New(),
                    name,
                    email,
                    passwordHash,
                    role,
                    clock.UtcNow,
                    NotificationSettings.Default
                );

            user.Raise(new UserRegisteredEvent
            (
                user.Id,
                user.Email.Value,
                user.Name.Display,
                user.Role,
                user.CreatedAtUtc
            ));
            return user;


        }

        public void RecordLogin(IClock clock)
        {

            LastLoginAtUtc = clock.UtcNow;

            Raise(new UserLoggedInEvent
                (
                Id,
                LastLoginAtUtc.Value 
            ));
        }

        public void ChangeRole(Role newRole, string ChangedBy,IClock clock)
        {
            if (Role == newRole) return;
            Role = newRole;
            
            Raise(new UserRoleChangedEvent
                (
                    Id,
                    Role,
                    newRole,
                    ChangedBy,
                    clock.UtcNow
                ));
        }

        public void UpdateNotificationSettings(NotificationSettings settings)
        {
            NotificationSettings = settings ?? NotificationSettings;
        }

        public bool VerifyPassword(string candidate) => PasswordHash.Verify(candidate);


        public void ChangePassword(string newPassword,IPasswordPolicy passwordPolicy)
        {
            if(!passwordPolicy.isSatisfiedBy(newPassword))
                throw new ArgumentException("Password does not meet the policy requirements");
            PasswordHash = PasswordHash.FromPlainText(newPassword, passwordPolicy);
        }

    }
}
