using GameLoanManager.Domain.Commands.Friends.CreateFriend;

namespace GameLoanManager.Domain.Test.Mocks.Commands.CreateFriend
{
    public class CreateFriendCommandMock
    {
        public static CreateFriendCommand GetDefaultValidInstance()
        {
            return new CreateFriendCommand("Miguel Padoze", "16912345678");
        }
        public static CreateFriendCommand GetEmptyNameInstance()
        {
            return new CreateFriendCommand(string.Empty, "16912345678");
        }
        public static CreateFriendCommand GetEmptyCellPhoneNumberInstance()
        {
            return new CreateFriendCommand("Miguel Padoze", string.Empty);
        }
        public static CreateFriendCommand GetCellPhoneNumberLeeThenElevenCharacteresLengthInstance()
        {
            return new CreateFriendCommand("Miguel Padoze", "1691234567");
        }
        public static CreateFriendCommand GetCellPhoneNumberMoreThenElevenCharacteresLengthInstance()
        {
            return new CreateFriendCommand("Miguel Padoze", "169123456789");
        }
        public static CreateFriendCommand GetNameMoreThenHundredCharacteresLengthInstance()
        {
            return new CreateFriendCommand(
                "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901", 
                "16912345678");
        }
    }
}
