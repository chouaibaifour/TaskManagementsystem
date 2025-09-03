using AutoMapper;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.ValueObjects;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ConstructUsing(u => new UserDto(
                u.Id.Value,
                u.Name.First,
                u.Name.Last,
                u.Email.Value,
                u.PasswordHash.Value,
                u.UserRole.ToEnum(),
                u.CreatedAtUtc,
                u.LastLoginAtUtc,
                u.NotificationSettings.EmailEnabled,
                u.NotificationSettings.PushEnabled
            ));

        
        CreateMap<UserDto, User>()
            .ConstructUsing(dto =>  User.Rehydrate(
                new UserId(dto.Id),
                FullName.Create(dto.FirstName, dto.LastName),
                Email.Create(dto.Email),
                PasswordHash.FromString(dto.PasswordHash),
                UserRole.FromEnum(dto.Role),
                dto.CreatedAtUtc,
                dto.LastLoginAtUtc,
                NotificationSettings.Create(dto.EmailEnabled, dto.PushEnabled)
                ));
}
}