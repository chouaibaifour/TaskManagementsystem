using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;

namespace TaskManagement.Domain.Tasks.ValueObjects.Comment
{
    public sealed  class CommentContent:Content
    {
        private const int MAX_LENGTH = 500;

        public CommentContent(string value) : base(value)
        {
            EnsureMaxLength(value, MAX_LENGTH);
            EnsureRequired(value);
        }

        public static CommentContent Create(string value) => new(value);
    }
}
