using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Events.GameReturned
{
    public class GameReturnedEventHandler : INotificationHandler<GameReturnedEvent>
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly ILogger<GameReturnedEventHandler> _logger;

        public GameReturnedEventHandler(
            IFriendRepository friendRepository,
            ILogger<GameReturnedEventHandler> logger,
            IBaseRepository<Game> gameRepository)
        {
            _logger = logger;
            _friendRepository = friendRepository;
            _gameRepository = gameRepository;
        }
        public async Task Handle(GameReturnedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GameReturnedEventHandler was called Notification.GameId: {notification.GameId}");

            var game = await _gameRepository.GetByIdAsync(notification.GameId);

            if (game == null)
            {
                _logger.LogInformation($"GameReturnedEventHandler The Return was not successful, the game Id don't exists {notification.GameId}");
                return;
            }

            await _friendRepository.UpdateOneRemoveLoanedGameAsync(game.Id, cancellationToken);

            _logger.LogInformation("GameReturnedEventHandler end of execution");
        }
    }
}
