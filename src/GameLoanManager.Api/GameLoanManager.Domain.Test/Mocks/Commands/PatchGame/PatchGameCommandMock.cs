using GameLoanManager.Domain.Commands.Games.PatchGame;
using GameLoanManager.Domain.Test.Mocks.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Commands.PatchGame
{
    public class PatchGameCommandMock
    {
        public static PatchGameCommand GetDefaultValidInstance()
        {
            return new PatchGameCommand(
                GameMock.ValidGameId,
                "Dark Souls 2"
                );
        }
        public static PatchGameCommand GetDefaultInstanceWithNonExistentGame()
        {
            return new PatchGameCommand(
                "abcdefghij",
                "Dark Souls 2");
        }
        public static PatchGameCommand GetNameMoreThenHundredCharacteresLengthInstance()
        {
            return new PatchGameCommand(
                GameMock.ValidGameId,
                "123456789012345678901234567890123456789012345678901"
                );
        }
    }
}
