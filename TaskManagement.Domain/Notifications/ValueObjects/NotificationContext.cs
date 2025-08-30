using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Notifications.ValueObjects
{
    public record NotificationContext(Guid EntityId, string EntityType)
    {
        public static NotificationContext Create
            (Guid entityId, string entityType) => new(entityId, entityType);
        public override string ToString() => $"{EntityType}:{EntityId}";

    }
}
