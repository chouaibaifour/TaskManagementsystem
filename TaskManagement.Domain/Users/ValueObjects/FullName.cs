using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.ValueObjects
{
    public sealed class FullName
    {
        public string First { get; }
        public string Last { get; }
        public string Display => string.IsNullOrWhiteSpace(Last) ? First : $"{First} {Last}";
        private FullName(string first, string last )
        {
            if (string.IsNullOrWhiteSpace(first)) throw new ArgumentException("First name Required");

            First = first.Trim();
            Last = last.Trim();
        }
        public static FullName Create(string first,string last="")=>new(first, last);

        public override string ToString()=> Display;
        
    }
}
