using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Test.Mocks.Entities
{
    public class FriendMock
    {

        public static readonly string ValidFriendId = "5fb281810430709bfc2f9add";
        public static Friend GetDefaultValidInstance()
        {
            return new Friend(ValidFriendId, "Miguel Patreze", "16987654321");
        }
    }
}
