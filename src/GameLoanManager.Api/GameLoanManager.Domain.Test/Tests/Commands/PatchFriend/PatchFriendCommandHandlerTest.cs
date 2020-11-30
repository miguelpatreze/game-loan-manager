using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Friends.PatchFriend;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks;
using GameLoanManager.Domain.Test.Mocks.Commands.PatchFriend;
using GameLoanManager.Domain.Test.Mocks.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.PatchFriend
{
    public class PatchFriendCommandHandlerTest
    {
        private readonly INotificationContext _notificationContext;

        public PatchFriendCommandHandlerTest()
        {
            var services = new ServiceCollection();
            services.AddScoped<INotificationContext, NotificationContext>();
            var serviceProvider = services.BuildServiceProvider();
            _notificationContext = serviceProvider.GetService<INotificationContext>();
        }

        public PatchFriendCommandHandler GetHandler(
            IMapper mapper = null,
            IFriendRepository repository = null,
            INotificationContext notificationContext = null,
            ILogger<PatchFriendCommandHandler> logger = null
        )
        {
            mapper ??= AutoMapperMock.GetDefaultInstance();
            repository ??= Substitute.For<IFriendRepository>();
            logger ??= Substitute.For<ILogger<PatchFriendCommandHandler>>();
            notificationContext ??= Substitute.For<INotificationContext>();

            return new PatchFriendCommandHandler(
                mapper,
                repository,
                logger,
                notificationContext);
        }
        [Fact(DisplayName = "Should Be Success When Call Method Handle")]
        public async Task ShouldBeSuccessWhenCallMethodHandle()
        {
            var repository = FriendRepositoryMock.GetDefaultInstance();
            var command = PatchFriendCommandMock.GetDefaultValidInstance();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.ReceivedWithAnyArgs().ReplaceOneAsync(default, default);
        }

        [Fact(DisplayName = "Should Not Be Success When Call Method Handle And Friend Is Null")]
        public async Task ShouldBeSuccessWhenCallMethodHandleAndFriendIsNull()
        {
            var repository = FriendRepositoryMock.GetDefaultInstance();
            var command = PatchFriendCommandMock.GetDefaultInstanceWithNonExistentFriendInstance();
            var handler = GetHandler(repository: repository, notificationContext: _notificationContext);

            await handler.Handle(command, default);

            await repository.DidNotReceiveWithAnyArgs().ReplaceOneAsync(default, default);
            Assert.True(_notificationContext.HasNotifications);
        }
    }
}
