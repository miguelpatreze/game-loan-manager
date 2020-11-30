using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Games.PatchGame;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks;
using GameLoanManager.Domain.Test.Mocks.Commands.PatchGame;
using GameLoanManager.Domain.Test.Mocks.Contracts;
using GameLoanManager.Domain.Test.Mocks.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.PatchGame
{
    public class PatchGameCommandHandlerTest
    {
        private readonly INotificationContext _notificationContext;

        public PatchGameCommandHandlerTest()
        {
            var services = new ServiceCollection();
            services.AddScoped<INotificationContext, NotificationContext>();
            var serviceProvider = services.BuildServiceProvider();
            _notificationContext = serviceProvider.GetService<INotificationContext>();
        }

        public PatchGameCommandHandler GetHandler(
            IMapper mapper = null,
            IBaseRepository<Game> repository = null,
            INotificationContext notificationContext = null,
            ILogger<PatchGameCommandHandler> logger = null
        )
        {
            mapper ??= AutoMapperMock.GetDefaultInstance();
            repository ??= Substitute.For<IBaseRepository<Game>>();
            logger ??= Substitute.For<ILogger<PatchGameCommandHandler>>();
            notificationContext ??= Substitute.For<INotificationContext>();

            return new PatchGameCommandHandler(
                mapper,
                repository,
                logger,
                notificationContext);
        }
        [Fact(DisplayName = "Should Be Success When Call Method Handle")]
        public async Task ShouldBeSuccessWhenCallMethodHandle()
        {
            var repository = GameRepositoryMock.GetDefaultInstance();
            var command = PatchGameCommandMock.GetDefaultValidInstance();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.ReceivedWithAnyArgs().ReplaceOneAsync(default, default);
        }

        [Fact(DisplayName = "Should Not Be Success When Call Method Handle And Game Is Null")]
        public async Task ShouldBeSuccessWhenCallMethodHandleAndGameIsNull()
        {
            var repository = GameRepositoryMock.GetDefaultInstance();
            var command = PatchGameCommandMock.GetDefaultInstanceWithNonExistentGame();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.DidNotReceiveWithAnyArgs().ReplaceOneAsync(default, default);
            Assert.True(_notificationContext.HasNotifications);
        }
    }
}
