
using GameLoanManager.Domain.Commands.Games.CreateGame;

namespace GameLoanManager.Domain.Test.Mocks.Commands.CreateGame
{
    public static class CreateGameCommandMock
    {

        public static CreateGameCommand GetDefaultValidInstance()
        {
            return new CreateGameCommand("Dark Souls 2");
        }
        public static CreateGameCommand GetEmptyNameInstance()
        {
            return new CreateGameCommand(string.Empty);
        }
        public static CreateGameCommand GetNameMoreThenFiftyCharacteresLengthInstance()
        {
            return new CreateGameCommand(
                "123456789012345678901234567890123456789012345678901"
                );
        }
    }
}
