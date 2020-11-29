using GameLoanManager.Domain.Commands.Games.PatchGame;
using GameLoanManager.Domain.Test.Mocks.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Commands.PatchGame
{
    public class PatchGameCommandMock
    {
        public static PatchGameCommand GetDefaultValidInstance()
        {
            return new PatchGameCommand(
                GameMock.GetDefaultValidInstance().Id,
                "Dark Souls 2"
                );
        }
        public static PatchGameCommand GetDefaultInstanceWithNonExistentGame()
        {
            return new PatchGameCommand(
                "abcdefghij",
                "Dark Souls 2");
        }
    }
}
