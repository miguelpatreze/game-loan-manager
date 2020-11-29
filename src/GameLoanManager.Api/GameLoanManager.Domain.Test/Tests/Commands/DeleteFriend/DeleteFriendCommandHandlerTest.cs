using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Friends.DeleteFriend;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks;
using GameLoanManager.Domain.Test.Mocks.Commands.DeleteFriend;
using GameLoanManager.Domain.Test.Mocks.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.DeleteFriend
{
    public class DeleteFriendCommandHandlerTest
    {
        private readonly INotificationContext _notificationContext;

        public DeleteFriendCommandHandlerTest()
        {
            var services = new ServiceCollection();
            services.AddScoped<INotificationContext, NotificationContext>();
            var serviceProvider = services.BuildServiceProvider();
            _notificationContext = serviceProvider.GetService<INotificationContext>();
        }

        public DeleteFriendCommandHandler GetHandler(
            IMapper mapper = null,
            IFriendRepository repository = null,
            INotificationContext notificationContext = null,
            ILogger<DeleteFriendCommandHandler> logger = null
        )
        {
            mapper ??= AutoMapperMock.GetDefaultInstance();
            repository ??= Substitute.For<IFriendRepository>();
            logger ??= Substitute.For<ILogger<DeleteFriendCommandHandler>>();
            notificationContext ??= Substitute.For<INotificationContext>();

            return new DeleteFriendCommandHandler(
                mapper,
                repository,
                logger,
                notificationContext);
        }
        [Fact(DisplayName = "Should Be Success When Call Method Handle")]
        public async Task ShouldBeSuccessWhenCallMethodHandle()
        {
            var repository = FriendRepositoryMock.GetDefaultInstance();
            var command = DeleteFriendCommandMock.GetDefaultValidInstance();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.ReceivedWithAnyArgs().DeleteOneAsync(default, default);
        }

        [Fact(DisplayName = "Should Not Be Success When Call Method Handle And Friend Is Null")]
        public async Task ShouldBeSuccessWhenCallMethodHandleAndFriendIsNull()
        {
            var repository = FriendRepositoryMock.GetDefaultInstance();
            var command = DeleteFriendCommandMock.GetDefaultInstanceWithNonExistentFriend();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.DidNotReceiveWithAnyArgs().DeleteOneAsync(default, default);
            Assert.True(_notificationContext.HasNotifications);
        }
    }
}
