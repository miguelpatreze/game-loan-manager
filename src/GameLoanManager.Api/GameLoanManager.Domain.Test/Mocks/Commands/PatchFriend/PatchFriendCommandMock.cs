using GameLoanManager.Domain.Commands.Friends.PatchFriend;
using GameLoanManager.Domain.Test.Mocks.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Commands.PatchFriend
{
    public class PatchFriendCommandMock
    {
        public static PatchFriendCommand GetDefaultValidInstance()
        {
            return new PatchFriendCommand(
                FriendMock.GetDefaultValidInstance().Id,
                "Miguel Patreze",
                "1698765-4321"
                );
        }
        public static PatchFriendCommand GetDefaultInstanceWithNonExistentFriend()
        {
            return new PatchFriendCommand(
                "abcdefghij",
                "Miguel Patreze",
                "1698765-4321");
        }
    }
}
