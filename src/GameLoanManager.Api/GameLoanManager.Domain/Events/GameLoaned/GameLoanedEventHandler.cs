using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Events.GameLoaned
{
    public class GameLoanedEventHandler : INotificationHandler<GameLoanedEvent>
    {
        private readonly IFriendRepository _friendRepository;
        private readonly ILogger<GameLoanedEventHandler> _logger;

        public GameLoanedEventHandler(
            IFriendRepository friendRepository,
            ILogger<GameLoanedEventHandler> logger)
        {
            _logger = logger;
            _friendRepository = friendRepository;
        }
        public async Task Handle(GameLoanedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GameLoanedEventHandler was called Notification.GameName: {notification.GameName} Notification.FriendId: {notification.FriendId}");

            var friend = await _friendRepository.GetByIdAsync(notification.FriendId);

            if (friend == null)
            {
                _logger.LogInformation($"GameLoanedEventHandler The Loan was not successful, the friend Id don't exists {notification.FriendId}");
                return;
            }

            friend.LoanGame(notification.GameId, notification.GameName, notification.LoanedAt);
            await _friendRepository.ReplaceOneAsync(friend);

            _logger.LogInformation("GameLoanedEventHandler end of execution");
        }
    }
}
