using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Common.Primitives.ValueObject
{
    public abstract class Content
    {
        private string Value;
       
        public  string Display => Value;

        protected Content(string value)
        {

            Value = value!.Trim();
        }


        protected void EnsureMaxLength(string value ,int MaxLength)
        {
            if (value.Length > MaxLength)
                throw new ArgumentException($"Content cannot exceed {MaxLength} characters.");
        }

        protected void EnsureRequired(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Content cannot be null or whiteSpace.");
        }

        public override string ToString() => Display;

        public static bool operator ==(Content a, Content b)
        {
            if (a.Equals( b))
                return true;
                       
            return a.Value == b.Value;
        }
        public static bool operator !=(Content a, Content b) => !(a == b);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            
                return true;
            

            if (ReferenceEquals(obj, null))
            
                return false;

            return false;
           
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
