using MediatR;

namespace GameLoanManager.Domain.Commands.Games.LoanGame
{
    public class LoanGameCommand : IRequest
    {
        public LoanGameCommand(string gameId, string friendId)
        {
            GameId = gameId;
            FriendId = friendId;
        }
        public string GameId { get; set; }
        public string FriendId { get; set; }
    }
}
