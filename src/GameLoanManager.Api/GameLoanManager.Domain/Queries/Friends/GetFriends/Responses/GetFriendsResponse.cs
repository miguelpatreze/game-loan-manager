using System;

namespace GameLoanManager.Domain.Queries.Friends.GetFriends.Responses
{
    public class GetFriendsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
