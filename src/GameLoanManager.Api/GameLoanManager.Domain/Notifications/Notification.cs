namespace GameLoanManager.Domain.Notifications
{
    public class Notification
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }

        public Notification(string key, string message, NotificationType type)
        {
            Key = key;
            Message = message;
            Type = type;
        }

    }
}
