using MediatR;

namespace GameLoanManager.Domain.Commands.Games.CreateGame
{
    public class CreateGameCommand : IRequest<string>
    {
        private CreateGameCommand()
        { }
        public CreateGameCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
