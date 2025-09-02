

namespace TaskManagement.Infrastructure.DTOs;

public class UserDto(
    Guid id,
    string firstName,
    string lastName,
    string email,
    string passwordHash,
    string role,
    DateTime createdAtUtc,
    DateTime? lastLoginAtUtc,
    bool emailEnabled,
    bool pushEnabled)
{
    public Guid Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public string PasswordHash { get; set; } = passwordHash;
    public string Role { get; set; } = role;
    public DateTime CreatedAtUtc { get; set; } = createdAtUtc;
    public DateTime? LastLoginAtUtc { get; set; } = lastLoginAtUtc;
    public bool EmailEnabled { get; set; } = emailEnabled;
    public bool PushEnabled { get; set; } = pushEnabled;
}