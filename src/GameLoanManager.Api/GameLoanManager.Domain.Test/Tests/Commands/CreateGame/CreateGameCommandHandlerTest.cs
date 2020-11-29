using AutoMapper;
using GameLoanManager.Domain.Commands.Games.CreateGame;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks;
using GameLoanManager.Domain.Test.Mocks.Commands.CreateGame;
using GameLoanManager.Domain.Test.Mocks.Contracts;
using GameLoanManager.Domain.Test.Mocks.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.CreateGame
{
    public class CreateGameCommandHandlerTest
    {
        public CreateGameCommandHandler GetHandler(
            IMapper mapper = null,
            IBaseRepository<Game> repository = null,
            ILogger<CreateGameCommandHandler> logger = null
        )
        {
            mapper ??= AutoMapperMock.GetDefaultInstance();
            repository ??= GameRepositoryMock.GetDefaultInstance();
            logger ??= Substitute.For<ILogger<CreateGameCommandHandler>>();

            return new CreateGameCommandHandler(
                mapper,
                repository,
                logger);
        }

        [Fact(DisplayName = "Should Be Success When Call Method Handle")]
        public async Task ShouldBeSuccessWhenCallMethodHandle()
        {
            var repository = GameRepositoryMock.GetDefaultInstance();
            var command = CreateGameCommandMock.GetDefaultValidInstance();
            var handler = GetHandler(repository: repository);

            var result = await handler.Handle(command, default);
            
            await repository.ReceivedWithAnyArgs().InsertOneAsync(default, default);
            Assert.Equal(result, GameMock.ValidGameId);
        }
    }
}
