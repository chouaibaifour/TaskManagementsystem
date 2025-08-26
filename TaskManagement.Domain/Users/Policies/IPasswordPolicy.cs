using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.Policies
{
    public interface IPasswordPolicy
    {
        bool isSatisfiedBy(string Password);
    }
}
