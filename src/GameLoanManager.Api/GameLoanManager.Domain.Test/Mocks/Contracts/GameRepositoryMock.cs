using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks.Entities;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Test.Mocks.Contracts
{
    public static class GameRepositoryMock
    {

        public static IBaseRepository<Game> GetDefaultInstance()
        {
            return Substitute.For<IBaseRepository<Game>>()
                    .GetInsertOneAsync();
        }
        public static IBaseRepository<Game> GetInsertOneAsync(this IBaseRepository<Game> repository)
        {
            repository.InsertOneAsync(Arg.Is<Game>(GameMock.GetDefaultValidInstance()), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            return repository;
        }
    }
}
