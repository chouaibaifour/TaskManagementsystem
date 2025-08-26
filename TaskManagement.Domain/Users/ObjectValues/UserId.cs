using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.ObjectValues
{
    public readonly record struct UserId(Guid Value)
    {
        public static UserId New() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();


        public static implicit operator Guid(UserId userId) => userId.Value;

        public static implicit operator UserId(Guid value) => new UserId(value);

    }
}
