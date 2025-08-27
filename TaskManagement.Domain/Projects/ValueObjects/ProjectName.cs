using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;

namespace TaskManagement.Domain.Projects.ValueObjects
{
    // project name must not be empty or whitespace and unique per user/team
    public sealed class ProjectName:Content
    {
        private const int MAX_LENGTH = 500;
        
        
        private ProjectName(string value) : base(value)
        {
            EnsureRequired(value);
            EnsureMaxLength(value, MAX_LENGTH);
            
        }

        public static ProjectName Create(string value) => new(value);
       
    }
}
