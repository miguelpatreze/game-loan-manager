using System;

namespace GameLoanManager.Domain.Queries.Games.GetGames.Responses
{
    public class GetGamesResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Loaned { get; set; }
    }
}
