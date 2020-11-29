using MediatR;
using System;

namespace GameLoanManager.Domain.Events.GameLoaned
{
    public class GameLoanedEvent : INotification
    {
        public GameLoanedEvent(string gameId, string gameName, string friendId, DateTime loanedAt)
        {
            GameId = gameId;
            GameName = gameName;
            FriendId = friendId;
            LoanedAt = loanedAt;
        }
        public string GameId { get; set; }
        public string GameName { get; set; }
        public string FriendId { get; set; }
        public DateTime LoanedAt { get; set; }
    }
}
