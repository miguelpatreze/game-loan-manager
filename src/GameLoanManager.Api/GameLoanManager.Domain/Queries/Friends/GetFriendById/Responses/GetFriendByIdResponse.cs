using System;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses
{
    public class GetFriendByIdResponse
    {
        public string Name { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
