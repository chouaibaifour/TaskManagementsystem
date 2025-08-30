using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;

namespace TaskManagement.Domain.Notifications.ValueObjects
{
    public sealed class NotificationTitle:Content
    {
        public const int MAX_LENGTH = 100;

        private NotificationTitle(string value) : base(value, nameof(NotificationTitle))
        {
            EnsureRequired(value );
            EnsureMaxLength(value, MAX_LENGTH);

        }
        public static NotificationTitle Create(string value) => new(value);


    }
}
