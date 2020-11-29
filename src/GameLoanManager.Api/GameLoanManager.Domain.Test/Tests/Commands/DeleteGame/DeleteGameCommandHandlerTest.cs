using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Games.DeleteGame;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks;
using GameLoanManager.Domain.Test.Mocks.Commands.DeleteGame;
using GameLoanManager.Domain.Test.Mocks.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.DeleteGame
{
    public class DeleteGameCommandHandlerTest
    {
        private readonly INotificationContext _notificationContext;

        public DeleteGameCommandHandlerTest()
        {
            var services = new ServiceCollection();
            services.AddScoped<INotificationContext, NotificationContext>();
            var serviceProvider = services.BuildServiceProvider();
            _notificationContext = serviceProvider.GetService<INotificationContext>();
        }

        public DeleteGameCommandHandler GetHandler(
            IMapper mapper = null,
            IBaseRepository<Game> repository = null,
            INotificationContext notificationContext = null,
            ILogger<DeleteGameCommandHandler> logger = null
        )
        {
            mapper ??= AutoMapperMock.GetDefaultInstance();
            repository ??= Substitute.For<IBaseRepository<Game>>();
            logger ??= Substitute.For<ILogger<DeleteGameCommandHandler>>();
            notificationContext ??= Substitute.For<INotificationContext>();

            return new DeleteGameCommandHandler(
                mapper,
                repository,
                logger,
                notificationContext);
        }
        [Fact(DisplayName = "Should Be Success When Call Method Handle")]
        public async Task ShouldBeSuccessWhenCallMethodHandle()
        {
            var repository = GameRepositoryMock.GetDefaultInstance();
            var command = DeleteGameCommandMock.GetDefaultValidInstance();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.ReceivedWithAnyArgs().DeleteOneAsync(default, default);
        }

        [Fact(DisplayName = "Should Not Be Success When Call Method Handle And Game Is Null")]
        public async Task ShouldBeSuccessWhenCallMethodHandleAndGameIsNull()
        {
            var repository = GameRepositoryMock.GetDefaultInstance();
            var command = DeleteGameCommandMock.GetDefaultInstanceWithNonExistentGame();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.DidNotReceiveWithAnyArgs().DeleteOneAsync(default, default);
            Assert.True(_notificationContext.HasNotifications);
        }
    }
}
