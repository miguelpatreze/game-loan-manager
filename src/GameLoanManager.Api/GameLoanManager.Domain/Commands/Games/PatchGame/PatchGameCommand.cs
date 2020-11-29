using MediatR;

namespace GameLoanManager.Domain.Commands.Games.PatchGame
{
    public class PatchGameCommand : IRequest
    {
        private  PatchGameCommand()
        { }
        public PatchGameCommand(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
