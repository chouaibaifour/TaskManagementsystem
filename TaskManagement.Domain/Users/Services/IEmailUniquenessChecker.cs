using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.Services
{
    public interface IEmailUniquenessChecker
    {
        Task<bool> IsUniqueAsync(string email, CancellationToken ct = default);
    }
}
