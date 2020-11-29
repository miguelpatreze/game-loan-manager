using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks.Entities;
using NSubstitute;
using System.Threading;

namespace GameLoanManager.Domain.Test.Mocks.Contracts
{
    public static class FriendRepositoryMock
    {


        public static IBaseRepository<Friend> GetDefaultInstance()
        {
            return Substitute.For<IBaseRepository<Friend>>()
                    .GetGetByIdAsync();
        }
        public static IBaseRepository<Friend> GetGetByIdAsync(this IBaseRepository<Friend> repository)
        {
            repository.GetByIdAsync(Arg.Is(FriendMock.GetDefaultValidInstance().Id), Arg.Any<CancellationToken>())
                .Returns(FriendMock.GetDefaultValidInstance());

            return repository;
        }
    }
}
