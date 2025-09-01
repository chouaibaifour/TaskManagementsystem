using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Users.interfaces;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.ValueObjects;


namespace TaskManagement.Infrastructure.Data.Json.Repositories
{
    internal class UserRepositoryJson : IUserRepository
    {
        
        private readonly FileStorage _Storage;

        public UserRepositoryJson(string filePath)
        {
            _Storage = new FileStorage(filePath);
        }

        public async Task AddAsync(User user)
        {
            var users = await _Storage.LoadAsync<User>();
            users.Add(user);
            await _Storage.SaveAsync(users);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            var users = await _Storage.LoadAsync<User>();

            return users.FirstOrDefault(u=>u.Email==email);

        }

        public async Task<User?> GetByIdAsync(UserId id)
        {
            var users = await _Storage.LoadAsync<User>();

            return users.FirstOrDefault(u => u.Id==id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _Storage.LoadAsync<User>();
        }

        public async Task UpdateAsync(User user)
        {
            var users = await _Storage.LoadAsync<User>();

            var index = users.FindIndex(u => u.Id == user.Id);
            if (index == -1)
                throw new InvalidOperationException($"User {user.Id} not found.");

            users[index] = user;
            await _Storage.SaveAsync(users);
        }

        public async Task<bool> DeleteAsync(UserId id)
        {
            var users = await _Storage.LoadAsync<User>();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                users.Remove(user);
                await _Storage.SaveAsync(users);
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsAsync(UserId id)
        {
            var users = await _Storage.LoadAsync<User>();

            return users.Any(u => u.Id == id);
            
        }
    }
}
