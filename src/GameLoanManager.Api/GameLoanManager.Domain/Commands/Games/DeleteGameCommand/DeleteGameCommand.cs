using MediatR;

namespace GameLoanManager.Domain.Commands.Games.DeleteGameCommand
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
