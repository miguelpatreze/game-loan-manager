using System;
using System.Collections.Generic;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses
{
    public class GetFriendByIdResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<GetFriendByIdLoanedGamesResponse> LoanedGames { get; set; }
    }
}
