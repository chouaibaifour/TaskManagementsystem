using AutoMapper;
using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Domain.Users;
using TaskManagement.Infrastructure.Data.Json.FileHandling;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.Data.Json.Repositories;

internal class UserRepositoryJson(UserFilePath userFilePath,IMapper mapper) : IUserRepository
{
    private readonly FileStorage _storage = new(userFilePath.Path);
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(User user)
    {
        var users =  await UserDtos();
        var userDto=_mapper.Map<User, UserDto>(user);
        
        users.Add(userDto);
        await _storage.SaveAsync(users);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
     var users =  await UserDtos();
     
     var userDto =  users.FirstOrDefault(u => u.Email == email);
     
     return (null == userDto) ? null : _mapper.Map<UserDto, User>(userDto);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var users =  await UserDtos();
        var userDto =  users.FirstOrDefault(u => u.Id == id);
        return (null == userDto) ? null : _mapper.Map<UserDto, User>(userDto);
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        var users = await UserDtos();
        return users.Select(u => _mapper.Map<UserDto, User>(u));
    }

    public async Task UpdateAsync(User user)
    {
        var users =  await UserDtos();

        var index = users.FindIndex(u => user.Id == u.Id);
        if (index == -1)
            throw new InvalidOperationException($"User {user.Id} not found.");

        users[index] = _mapper.Map<User, UserDto>(user);
        await _storage.SaveAsync(users);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var users = await UserDtos();
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user is not null)
        {
            users.Remove(user);
            await _storage.SaveAsync(users);
            return true;
        }

        return false;
    }

    private async Task<List<UserDto>> UserDtos()
    {
        var users = await _storage.LoadAsync<UserDto>();
        return users;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var users =  await UserDtos();

        return users.Any(u => u.Id == id);
    }
}