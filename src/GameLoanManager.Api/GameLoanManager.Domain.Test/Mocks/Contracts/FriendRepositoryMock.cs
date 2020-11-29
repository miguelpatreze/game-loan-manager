using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks.Entities;
using NSubstitute;
using System.Threading;

namespace GameLoanManager.Domain.Test.Mocks.Contracts
{
    public static class FriendRepositoryMock
    {


        public static IFriendRepository GetDefaultInstance()
        {
            return Substitute.For<IFriendRepository>()
                    .GetGetByIdAsync();
        }
        public static IFriendRepository GetGetByIdAsync(this IFriendRepository repository)
        {
            repository.GetByIdAsync(Arg.Is(FriendMock.ValidFriendId), Arg.Any<CancellationToken>())
                .Returns(FriendMock.GetDefaultValidInstance());

            return repository;
        }
    }
}
