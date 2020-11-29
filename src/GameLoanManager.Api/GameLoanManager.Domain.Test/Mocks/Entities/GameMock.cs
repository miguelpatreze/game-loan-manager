using GameLoanManager.Domain.Entities;
using MongoDB.Bson;

namespace GameLoanManager.Domain.Test.Mocks.Entities
{
    public static class GameMock
    {
        private static readonly string ValidGameId = ObjectId.GenerateNewId().ToString();
        public static Game GetDefaultValidInstance()
        {
            return new Game(ValidGameId, "Dark Souls 3");
        }
    }
}
