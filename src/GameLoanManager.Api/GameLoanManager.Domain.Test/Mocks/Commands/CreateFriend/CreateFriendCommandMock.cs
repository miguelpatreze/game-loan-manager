using GameLoanManager.Domain.Commands.Friends.CreateFriend;

namespace GameLoanManager.Domain.Test.Mocks.Commands.CreateFriend
{
    public class CreateFriendCommandMock
    {
        public static CreateFriendCommand GetDefaultValidInstance()
        {
            return new CreateFriendCommand("Miguel Padoze", "16912345678");
        }
    }
}
