using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GameLoanManager.Domain.ValueObjects
{
    public class LoanedGame
    {
        public LoanedGame(string gameId, string gameName, DateTime loanedAt)
        {
            GameId = gameId;
            GameName = gameName;
            LoanedAt = loanedAt;
        }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string GameId { get; set; }
        public string GameName { get; private set; }
        public DateTime LoanedAt { get; private set; }
    }
}
