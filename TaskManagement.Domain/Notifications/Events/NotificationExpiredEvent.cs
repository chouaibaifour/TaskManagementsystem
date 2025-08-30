using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Notifications.ValueObjects;

namespace TaskManagement.Domain.Notifications.Events
{
    public sealed record NotificationExpiredEvent(
        NotificationId NotificationId, DateTime ExpiredAt);


}
