using GameLoanManager.Domain.Commands.Friends.DeleteFriend;
using GameLoanManager.Domain.Test.Mocks.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Commands.DeleteFriend
{
    public class DeleteFriendCommandMock
    {
        public static DeleteFriendCommand GetDefaultValidInstance()
        {
            return new DeleteFriendCommand(FriendMock.GetDefaultValidInstance().Id);
        }
        public static DeleteFriendCommand GetDefaultInstanceWithNonExistentFriend()
        {
            return new DeleteFriendCommand("abcdefghij");
        }
    }
}
