namespace GameLoanManager.CrossCutting.Notification
{
    public class Notification
    {
        public Notification(string key, string message)
        {
            Key = key;
            ErrorMessage = message;
        }
        public string Key { get; }
        public string ErrorMessage { get; }
    }
}
