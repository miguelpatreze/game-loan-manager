using GameLoanManager.Domain.Notifications;
using System.Collections.Generic;

namespace GameLoanManager.Domain.Reponses
{
    public class Response
    {
        public object Payload { get; set; }
        public List<Notification> Notifications { get; set; }

        public Response() => Notifications = new List<Notification>();
        public Response(object payload, List<Notification> notifications)
        {
            Payload = payload;
            Notifications = notifications;
        }
        public void AddNotification(string key, string message, NotificationType type = NotificationType.Error)
        {
            Notifications.Add(new Notification(key, message, type));
        }

        public void AddNotifications(IList<Notification> notifications)
        {
            Notifications.AddRange(notifications);
        }
    }
}
