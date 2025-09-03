using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Common.Services;
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
        public UserRole UserRole { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastLoginAtUtc { get; private set; }
        public NotificationSettings NotificationSettings { get; private set; }
        private User(
            UserId id,
            FullName name,
            Email email,
            PasswordHash passwordHash,
            UserRole userRole,
            DateTime createdAtUtc,
            DateTime? lastLoginAtUtc,
            NotificationSettings notificationSettings)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            UserRole = userRole;
            CreatedAtUtc = createdAtUtc;
            LastLoginAtUtc = lastLoginAtUtc;
            NotificationSettings = notificationSettings;
        }

        public static User Rehydrate(
            UserId id,
            FullName name,
            Email email,
            PasswordHash passwordHash,
            UserRole userRole,
            DateTime createdAtUtc,
            DateTime? lastLoginAtUtc,
            NotificationSettings notificationSettings)
            => new User(id, name, email, passwordHash, userRole, createdAtUtc, lastLoginAtUtc, notificationSettings);
        
        public static User RegisterAsync
        (
            FullName name,
            Email email,
            PasswordHash passwordHash,
            UserRole userRole,
            IClock clock
           
        )
        {

            var user = new User
                (
                    UserId.New(),
                    name,
                    email,
                    passwordHash,
                    userRole,
                    clock.UtcNow,
                    null,
                    NotificationSettings.Default
                );

            user.Raise(new UserRegisteredEvent
            (
                user.Id,
                user.Email.Value,
                user.Name.Display,
                user.UserRole,
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

        public void ChangeRole(UserRole newUserRole, string changedBy,IClock clock)
        {
            if (UserRole == newUserRole) return;
            UserRole = newUserRole;
            
            Raise(new UserRoleChangedEvent
                (
                    Id,
                    UserRole,
                    newUserRole,
                    changedBy,
                    clock.UtcNow
                ));
        }

        public void UpdateNotificationSettings(NotificationSettings? settings)
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
