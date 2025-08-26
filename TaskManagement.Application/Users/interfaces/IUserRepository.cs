using TaskManagement.Domain.Users;
using TaskManagement.Domain.Users.ObjectValues;

namespace TaskManagement.Application.Users.interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByIdAsync(UserId id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
        Task UpdateAsync(User user, CancellationToken ct = default);
        Task<IEnumerable<User>> ListAsync(CancellationToken ct = default);
        Task <bool> DeleteAsync(UserId id, CancellationToken ct = default);
        Task<bool> ExistsAsync(UserId id, CancellationToken ct = default);
    }
}
