using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.Policies
{
    public sealed class DefaultPasswordPolicy:IPasswordPolicy
    {
        public static readonly Regex Pattern = new(
            @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!@#$%^&*()_+\-=]{8,}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public bool isSatisfiedBy(string Password)
            => !string.IsNullOrWhiteSpace(Password) && Pattern.IsMatch(Password);

    }
}
