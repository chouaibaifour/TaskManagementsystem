using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.ObjectValues
{
    public static class Guard
    {
        public static void AgainstNull<T>(T? input,string name)
        {
            if (input == null) throw new ArgumentNullException(name);
        }
        public static void AgainstNullOrWhiteSpace(string? input,string name)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException($"{name} is required.", name);
        }
    }
}
