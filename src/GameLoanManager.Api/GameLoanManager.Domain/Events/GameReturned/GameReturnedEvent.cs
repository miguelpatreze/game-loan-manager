using MediatR;

namespace GameLoanManager.Domain.Events.GameReturned
{
    public class GameReturnedEvent : INotification
    {
        public GameReturnedEvent(string gameId)
        {
            GameId = gameId;
        }
        public string GameId { get; set; }
    }
}
