using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.ObjectValues
{
    public sealed class Description
    {
        private const int MAX_LENGTH = 500;
        private string Value;
        public string Display => Value;
        private Description(string value)
        {
            if (value is null) throw new ArgumentNullException("Description cannot be null.");
            if (value.Length > MAX_LENGTH) throw new ArgumentException($"Description cannot be longer than {MAX_LENGTH} characters.");
            Value = value.Trim();
        }
        public static Description Create(string value) => new (value);
        public override string ToString() => Display;
    }
}
