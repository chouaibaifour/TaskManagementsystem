using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.ObjectValues
{
    // project name must not be empty or whitespace and unique per user/team
    public sealed class Name
    {
        private string Value;
        public string Display => Value;
        private Name(string vlaue)
        {
            if(string.IsNullOrWhiteSpace(vlaue)) throw new ArgumentException("Project name required");
            Value = vlaue.Trim();
        }

        public static Name Create(string value) => new(value);
        public override string ToString() => Display;
    }
}
