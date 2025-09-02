using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks
{
    public sealed class Comment
    {
        public CommentId CommentId { get; private set; }
        public UserId AuthorId { get; private set; }
        public CommentContent Content { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        private DateTime? UpdatedAtUtc { get; set; }

        private Comment
        (
            CommentId commentId,
            UserId authorId,
            CommentContent content,
            DateTime createdAtUtc,
            DateTime? updatedAtUtc
        )
        {
            CommentId = commentId;
            AuthorId = authorId;
            Content = content;
            CreatedAtUtc = createdAtUtc;
            UpdatedAtUtc = updatedAtUtc;
        }

         public static Comment Create
         (
            CommentId commentId,
            UserId authorId,
            CommentContent content,
            DateTime createdAtUtc
         )
         {
            return new Comment
            (
                commentId,
                authorId,
                content,
                createdAtUtc,
                null
            );
         }
        public void Update(CommentContent newContent,IClock clock)
        {
            AgainstSameContent(newContent);
               
            Content = newContent;
            UpdatedAtUtc = clock.UtcNow;

        }

        private void AgainstSameContent(CommentContent newContent)
        {
            if (newContent == Content)
                throw new Exception("New content is the same as the current content");
        }

        public override string ToString()=>
            $"{CommentId}-{AuthorId}-{Content}-{CreatedAtUtc}-{UpdatedAtUtc}";
        
            
        

    }
}
