using AutoMapper;
using GameLoanManager.Domain.Commands.Friends.CreateFriend;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks;
using GameLoanManager.Domain.Test.Mocks.Commands.CreateFriend;
using GameLoanManager.Domain.Test.Mocks.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.CreateFriend
{
    public class CreateFriendCommandHandlerTest
    {

        public CreateFriendCommandHandler GetHandler(
            IMapper mapper = null,
            IBaseRepository<Friend> repository = null,
            ILogger<CreateFriendCommandHandler> logger = null
        )
        {
            mapper ??= AutoMapperMock.GetDefaultInstance();
            repository ??= Substitute.For<IBaseRepository<Friend>>();
            logger ??= Substitute.For<ILogger<CreateFriendCommandHandler>>();

            return new CreateFriendCommandHandler(
                mapper,
                repository,
                logger);
        }

        [Fact(DisplayName = "Should Be Success When Call Method Handle")]
        public async Task ShouldBeSuccessWhenCallMethodHandle()
        {
            var repository = Substitute.For<IBaseRepository<Friend>>();
            var command = CreateFriendCommandMock.GetDefaultValidInstance();
            var handler = GetHandler(repository: repository);

            var result = await handler.Handle(command, default);

            await repository.ReceivedWithAnyArgs().InsertOneAsync(default, default);
            Assert.Equal(result, FriendMock.GetDefaultValidInstance().Id);
        }
    }
}
