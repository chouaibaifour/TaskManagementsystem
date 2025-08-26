using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.ObjectValues;
using TaskManagement.Domain.Users.Repositories;

namespace TaskManagement.Infrastructure.Data.Json.Repositories
{
    internal class UserRepositoryJson : IUserRepository
    {
        
        private readonly FileStorage _Storage;

        public UserRepositoryJson(string filePath)
        {
            _Storage = new FileStorage(filePath);
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            var users = await _Storage.LoadAsync<User>();
            users.Add(user);
            await _Storage.SaveAsync(users);
        }

        public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
        {
            var users = await _Storage.LoadAsync<User>();

            return users.FirstOrDefault(u=>u.Email==email);

        }

        public async Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default)
        {
            var users = await _Storage.LoadAsync<User>();

            return users.FirstOrDefault(u => u.Id==id);
        }

        public async Task<IEnumerable<User>> ListAsync(CancellationToken ct = default)
        {
            return await _Storage.LoadAsync<User>();
        }

        public async Task UpdateAsync(User user, CancellationToken ct = default)
        {
            var users = await _Storage.LoadAsync<User>();

            var index = users.FindIndex(u => u.Id == user.Id);
            if (index == -1)
                throw new InvalidOperationException($"User {user.Id} not found.");

            users[index] = user;
            await _Storage.SaveAsync(users);
        }

        public async Task DeleteAsync(UserId id, CancellationToken ct = default)
        {
            var users = await _Storage.LoadAsync<User>();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                users.Remove(user);
                await _Storage.SaveAsync(users);
            }
        }
    }
}
