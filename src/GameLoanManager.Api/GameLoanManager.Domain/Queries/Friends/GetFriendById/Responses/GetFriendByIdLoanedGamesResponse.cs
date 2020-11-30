using System;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses
{
    public class GetFriendByIdLoanedGamesResponse
    {
        public string GameId { get; set; }
        public string Name { get; set; }
        public DateTime LoanedAt { get; set; }
    }
}
