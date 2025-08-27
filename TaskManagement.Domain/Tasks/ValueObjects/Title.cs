using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;

namespace TaskManagement.Domain.Tasks.ValueObjects
{
    public sealed class Title:Content
    {
        
        private const int MAX_LENGTH = 100;

        public Title(string value ) : base(value)
        {
            EnsureRequired(value);
            EnsureMaxLength(value,MAX_LENGTH);

        }

        public static Title Create(string value) => new(value);

    }
}
