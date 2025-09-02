using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Domain.Users;
using TaskManagement.Infrastructure.Data.Json.FileHandling;

namespace TaskManagement.Infrastructure.Data.Json.Repositories;

internal class UserRepositoryJson(UserFilePath userFilePath) : IUserRepository
{
    private readonly FileStorage _storage = new(userFilePath.Path);

    public async Task AddAsync(User user)
    {
        var users = await _storage.LoadAsync<User>();
        users.Add(user);
        await _storage.SaveAsync(users);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var users = await _storage.LoadAsync<User>();

        return users.FirstOrDefault(u => u.Email == email);
    }



    public async Task<User?> GetByIdAsync(Guid id)
    {
        var users = await _storage.LoadAsync<User>();

        return users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _storage.LoadAsync<User>();
    }

    public async Task UpdateAsync(User user)
    {
        var users = await _storage.LoadAsync<User>();

        var index = users.FindIndex(u => u.Id == user.Id);
        if (index == -1)
            throw new InvalidOperationException($"User {user.Id} not found.");

        users[index] = user;
        await _storage.SaveAsync(users);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var users = await _storage.LoadAsync<User>();
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user is not null)
        {
            users.Remove(user);
            await _storage.SaveAsync(users);
            return true;
        }

        return false;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var users = await _storage.LoadAsync<User>();

        return users.Any(u => u.Id == id);
    }
}