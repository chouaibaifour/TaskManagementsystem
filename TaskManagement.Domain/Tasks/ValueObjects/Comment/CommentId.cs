using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Tasks.ValueObjects.Comment
{
    public readonly record struct CommentId(Guid Value)
    {
        public static CommentId New() => new(Guid.NewGuid());

        public override string ToString() => Value.ToString();

        public static implicit operator Guid(CommentId CommentId) => CommentId.Value;

        public static implicit operator CommentId(Guid Value) => new(Value);
    }
}
