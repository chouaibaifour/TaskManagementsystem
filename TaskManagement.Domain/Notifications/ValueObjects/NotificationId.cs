using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Notifications.ValueObjects
{
   public readonly record struct  NotificationId(Guid Value)
    {
        public static NotificationId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
        public static implicit operator Guid(NotificationId notificationId) => notificationId.Value;
        public static implicit operator NotificationId(Guid Value) => new(Value);
    }
    
}
