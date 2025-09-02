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
    public Guid Id { get; init; } = id;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string Email { get; init; } = email;
    public string PasswordHash { get; init; } = passwordHash;
    public string Role { get; init; } = role;
    
    public DateTime CreatedAtUtc { get; init; } = createdAtUtc;
    
    public DateTime? LastLoginAtUtc { get; init; } = lastLoginAtUtc;
    public bool EmailEnabled { get; init; } = emailEnabled;
    public bool PushEnabled { get; init; } = pushEnabled;
}