
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Users.Enums;
using TaskManagement.Domain.Users.ObjectValues;
using TaskManagement.Domain.Users.Policies;
using TaskManagement.Domain.Users.Events;
using TaskManagement.Domain.Users.Services;

namespace TaskManagement.Domain.Users
{
    public  class User:AggregateRoot<UserId>
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

        public static Task<User>RegisterAsync
        (
            FullName name,
            Email email,
            PasswordHash passwordHash,
            Role role,
            DateTime nowUtc,
            CancellationToken ct = default
        )
        {

            var user = new User
                (
                    UserId.New(),
                    name,
                    email,
                    passwordHash,
                    role,
                    nowUtc,
                    NotificationSettings.Default
                );

            user.Raise(new UserRegisteredEvent
            (
                user.Id,
                user.Email.Value,
                user.Name.Display,
                user.Role,
                nowUtc
            ));
            return Task.FromResult(user);

            
        }

        public void RecordLogin(DateTime nowUtc)
        {

            LastLoginAtUtc = nowUtc;

            Raise(new UserLoggedInEvent
                (
                Id,
                nowUtc
            ));
        }

        public void ChangeRole(Role newRole,string ChangedBy)
        {
            if(Role == newRole) return;
            Role = newRole;
            Raise(new UserRoleChangedEvent
                (
                    Id,
                    Role,
                    newRole,
                    ChangedBy,
                    DateTime.UtcNow
                ));
        }

        public void UpdateNotificationSettings(NotificationSettings settings)
        {
            NotificationSettings = settings ?? NotificationSettings;
        }

        public bool VerifyPassword(string candidate) => PasswordHash.Verify(candidate);

    }
}
