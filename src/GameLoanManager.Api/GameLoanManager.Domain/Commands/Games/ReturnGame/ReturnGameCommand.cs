using MediatR;

namespace GameLoanManager.Domain.Commands.Games.ReturnGame
{
    public class ReturnGameCommand : IRequest
    {
        public ReturnGameCommand(string gameId)
        {
            GameId = gameId;
        }
        public string GameId { get; set; }
    }
}
