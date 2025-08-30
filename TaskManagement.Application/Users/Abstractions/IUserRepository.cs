using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByIdAsync(UserId id );
        Task<User?> GetByEmailAsync(Email email );
        Task AddAsync(User user );
        Task UpdateAsync(User user );
        Task<IEnumerable<User>> ListAsync();
        Task <bool> DeleteAsync(UserId id );
        Task<bool> ExistsAsync(UserId id );
    }
}
