using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;

namespace TaskManagement.Domain.Notifications.ValueObjects
{
    public sealed class NotificationMessage : Content
    {
        public const int MAX_LENGTH = 500;
        private NotificationMessage(string value) : base(value, nameof(NotificationMessage))
        {

            EnsureMaxLength(value, MAX_LENGTH);
        }
        public static NotificationMessage Create(string value) => new(value);
    }
}
