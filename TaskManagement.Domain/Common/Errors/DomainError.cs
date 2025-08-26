using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Common.Errors
{
    public sealed record DomainError(string code,string message)
    {
        public override string ToString() => $"{code} : {message}";
    }
}
