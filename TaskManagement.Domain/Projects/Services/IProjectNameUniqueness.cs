using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.Services
{
    public interface IProjectNameUniqueness
    {
        Task<bool> IsUniqueAsync(string name, CancellationToken ct = default);
    }
}
