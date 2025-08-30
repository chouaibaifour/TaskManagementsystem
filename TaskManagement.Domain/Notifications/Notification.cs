using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Notifications.Enums;
using TaskManagement.Domain.Notifications.Events;
using TaskManagement.Domain.Notifications.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Notifications
{
    public class Notification:AggregateRoot<NotificationId>
    {
        public UserId UserId { get; private set; }
        public NotificationTitle NotificationTitle { get; private set; }
        public NotificationMessage NotificationMessage { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public bool IsRead { get; private set; } = false;
        public NotificationContext NotificationContext { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ReadAt { get; private set; }

        public Notification
        (
            NotificationId id,
            UserId userId, 
            NotificationTitle notificationTitle, 
            NotificationMessage notificationMessage, 
            NotificationType notificationType, 
            bool isRead, 
            NotificationContext notificationContext, 
            DateTime createdAt, 
            DateTime? readAt
        )
        {
            Id = id;
            UserId = userId;
            NotificationTitle = notificationTitle;
            NotificationMessage = notificationMessage;
            NotificationType = notificationType;
            IsRead = isRead;
            NotificationContext = notificationContext;
            CreatedAt = createdAt;
            ReadAt = readAt;
        }
        public static Notification Create
        (
            UserId userId,
            NotificationTitle notificationTitle,
            NotificationMessage notificationMessage,
            NotificationType notificationType,
            NotificationContext notificationContext,
            IClock clock
        )
        {
            var notification= new Notification
            (
                NotificationId.New(),
                userId,
                notificationTitle,
                notificationMessage,
                notificationType,
                false,
                notificationContext,
                clock.UtcNow,
                null
            );

            notification.Raise(new NotificationCreatedEvent(notification.Id, notification.CreatedAt));
            return notification;
        }

        public void MarkAsRead(IClock clock)
        {
            if (IsRead) return;
            IsRead = true;
            ReadAt = clock.UtcNow;
            Raise(new NotificationReadEvent(Id, ReadAt.Value));
        }
        public void MarkAsUnread()
        {
            if (!IsRead) return;
            IsRead = false;
            ReadAt = null;
        }
        public void MarkAsDelivered(IClock clock)
        {
            if (IsRead) return;
            
            Raise(new NotificationDeliveredEvent(Id,clock.UtcNow));
        }

        public void Archive(IClock clock)
        {
            Raise(new NotificationArchivedEvent(Id, clock.UtcNow));
        }

        public void Expire(IClock clock)
        {
            Raise(new NotificationExpiredEvent(Id, clock.UtcNow));
        }

    }
}
