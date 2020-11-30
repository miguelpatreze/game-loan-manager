using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.ValueObjects;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.MongoDB.Repositories
{
    public class FriendRepository : BaseRepository<Friend>, IFriendRepository
    {
        public FriendRepository(IMongoClient mongoClient, MongoSettings settings)
           : base(mongoClient, settings)
        {
        }
        public async Task UpdateOneRemoveLoanedGameAsync(string gameId, CancellationToken cancellationToken)
        {
            var filter = Builders<Friend>.Filter.ElemMatch(x => x.LoanedGames, loanedGame => loanedGame.GameId == gameId);
            var update = Builders<Friend>.Update.PullFilter(x => x.LoanedGames, Builders<LoanedGame>.Filter.Eq(loanedGame => loanedGame.GameId, gameId));

            await _collection.UpdateOneAsync(filter, update, null, cancellationToken);
        }
        public async Task<Friend> GetByGameIdAsync(string gameId, CancellationToken cancellationToken)
        {
            var filter = Builders<Friend>.Filter.ElemMatch(x => x.LoanedGames, loanedGame => loanedGame.GameId == gameId);

            return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
