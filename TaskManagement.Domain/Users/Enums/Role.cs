using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.Enums
{
    public enum Role
    {
        Member = 0,
        Manager =1,
        Admin = 2,
        Default = Member
    }
}
