namespace GameLoanManager.Api.Responses
{
    public class Response
    {
        public Response(object data = null, object notifications = null)
        {
            Data = data;
            Notifications = notifications;
        }

        public object Data { get; }
        public object Notifications { get; set; }
    }
}
