using System;

namespace GameLoanManager.Domain.Queries.Games.GetGameById.Responses
{
    public class GetGameByIdResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
