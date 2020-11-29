using GameLoanManager.Domain.Commands.Friends.PatchFriend;
using GameLoanManager.Domain.Test.Mocks.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Commands.PatchFriend
{
    public class PatchFriendCommandMock
    {
        public static PatchFriendCommand GetDefaultValidInstance()
        {
            return new PatchFriendCommand(
                FriendMock.ValidFriendId,
                "Miguel Patreze",
                "16987654321"
                );
        }
        public static PatchFriendCommand GetDefaultInstanceWithNonExistentFriendInstance()
        {
            return new PatchFriendCommand(
                "abcdefghij",
                "Miguel Patreze",
                "16987654321");
        }
        public static PatchFriendCommand GetEmptyIdInstance()
        {
            return new PatchFriendCommand(
                string.Empty,
                "Miguel Patreze",
                "16987654321");
        }
        public static PatchFriendCommand GetCellPhoneNumberLessThenElevenCharacteresLengthInstance()
        {
            return new PatchFriendCommand(
                FriendMock.ValidFriendId, 
                "Miguel Padoze", 
                "1691234567");
        }
        public static PatchFriendCommand GetCellPhoneNumberMoreThenElevenCharacteresLengthInstance()
        {
            return new PatchFriendCommand(
                FriendMock.ValidFriendId, 
                "Miguel Padoze", 
                "169123456789");
        }
        public static PatchFriendCommand GetNameMoreThenHundredCharacteresLengthInstance()
        {
            return new PatchFriendCommand(
                FriendMock.ValidFriendId,
                "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
                "1691234567");
        }
    }
}
