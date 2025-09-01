using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByIdAsync(Guid id );
        Task<User?> GetByEmailAsync(string email );
        Task AddAsync(User user );
        Task UpdateAsync(User user );
        Task<IEnumerable<User>> ListAsync();
        Task <bool> DeleteAsync(Guid id );
        Task<bool> ExistsAsync(Guid id );
    }
}
