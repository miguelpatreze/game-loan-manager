using System.Collections.Generic;

namespace GameLoanManager.CrossCutting.Notification
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
    }
}
