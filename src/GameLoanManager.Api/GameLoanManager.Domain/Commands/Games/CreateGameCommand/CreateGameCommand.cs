using MediatR;

namespace GameLoanManager.Domain.Commands.Games.CreateGameCommand
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
