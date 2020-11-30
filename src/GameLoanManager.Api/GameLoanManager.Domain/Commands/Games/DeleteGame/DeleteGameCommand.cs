using MediatR;

namespace GameLoanManager.Domain.Commands.Games.DeleteGame
{
    public class DeleteGameCommand : IRequest
    {
        public DeleteGameCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
