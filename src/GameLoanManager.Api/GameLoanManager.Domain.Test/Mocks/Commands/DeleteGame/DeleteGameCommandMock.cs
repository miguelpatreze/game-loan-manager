using GameLoanManager.Domain.Commands.Games.DeleteGame;
using GameLoanManager.Domain.Test.Mocks.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Commands.DeleteGame
{
    public class DeleteGameCommandMock
    {

        public static DeleteGameCommand GetDefaultValidInstance()
        {
            return new DeleteGameCommand(GameMock.GetDefaultValidInstance().Id);
        }
        public static DeleteGameCommand GetDefaultInstanceWithNonExistentGame()
        {
            return new DeleteGameCommand("abcdefghij");
        }
    }
}
