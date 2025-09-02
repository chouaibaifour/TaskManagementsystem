using AutoMapper;
using TaskManagement.Domain.Users;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}