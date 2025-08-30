using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Common.Services
{
    public class DefaultClockService : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
