
using GameLoanManager.Domain.Commands.Games.CreateGame;

namespace GameLoanManager.Domain.Test.Mocks.Commands.CreateGame
{
    public static class CreateGameCommandMock
    {

        public static CreateGameCommand GetDefaultValidInstance()
        {
            return new CreateGameCommand("Dark Souls 2");
        }
    }
}
