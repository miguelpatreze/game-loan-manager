using GameLoanManager.Domain.Entities;
using MongoDB.Bson;

namespace GameLoanManager.Domain.Test.Mocks.Entities
{
    public class FriendMock
    {

        private static readonly string ValidFriendId = ObjectId.GenerateNewId().ToString();
        public static Friend GetDefaultValidInstance()
        {
            return new Friend(ValidFriendId, "Dark Souls 3");
        }
    }
}
