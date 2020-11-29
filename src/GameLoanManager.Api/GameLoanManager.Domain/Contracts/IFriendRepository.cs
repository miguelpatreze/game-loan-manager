using GameLoanManager.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Contracts
{
    public interface IFriendRepository: IBaseRepository<Friend>
    {
        Task UpdateOneRemoveLoanedGameAsync(string gameId, CancellationToken cancellationToken);
    }
}
