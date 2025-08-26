using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Users.ObjectValues
{
    public sealed class NotificationSettings
    {
        public bool EmailEnabled { get; }
        public bool PushEnabled { get; }

        private NotificationSettings(bool emailEnabled,bool pushEnabled)
        {
            EmailEnabled = emailEnabled;
            PushEnabled = pushEnabled;
        }
        public static NotificationSettings Default => new(true, true);
        public static NotificationSettings Create(bool emailEnabled,bool pushEnabled)
                                    =>new(emailEnabled,pushEnabled);
    }
}
