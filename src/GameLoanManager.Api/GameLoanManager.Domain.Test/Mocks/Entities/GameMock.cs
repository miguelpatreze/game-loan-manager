using GameLoanManager.Domain.Entities;
using MongoDB.Bson;

namespace GameLoanManager.Domain.Test.Mocks.Entities
{
    public static class GameMock
    {
        public static readonly string ValidGameId = "5fb281810430709bfc2f9ade";
        public static Game GetDefaultValidInstance()
        {
            return new Game(ValidGameId, "Dark Souls 3");
        }
    }
}
