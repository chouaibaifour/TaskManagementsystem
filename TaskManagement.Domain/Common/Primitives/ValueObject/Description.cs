using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TaskManagement.Domain.Common.Primitives.ValueObject
{
    public sealed class Description:Content
    {
        private const int MAX_LENGTH = 500;

        private Description(string value) : base(value)
        {
            
            EnsureMaxLength(value, MAX_LENGTH);
          
        }

        public static Description Create(string value)=>new (value);

        

    }
}
