using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Events.GameLoaned;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Games.LoanGame
{
    public class LoanGameCommandHandler :
        IRequestHandler<LoanGameCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly ILogger<LoanGameCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;
        private readonly IMediator _mediator;

        public LoanGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> gameRepository,
            IFriendRepository friendRepository,
            ILogger<LoanGameCommandHandler> logger,
            INotificationContext notificationContext,
            IMediator mediator)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _logger = logger;
            _notificationContext = notificationContext;
            _friendRepository = friendRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(LoanGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateGameLoanCommandHandler was called Request.GameId: {request.GameId} Request.FriendId: {request.FriendId}");

            var game = await _gameRepository.GetByIdAsync(request.GameId);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.GameId} não foi encontrado.");
                await Unit.Task;
            }

            var friend = await _friendRepository.GetByIdAsync(request.FriendId);

            if (friend == null)
            {
                _notificationContext.AddNotification("Amigo não encontrado", $"O amigo com o id:{request.FriendId} não foi encontrado.");
                await Unit.Task;
            }

            game.LoanGame();
            await _gameRepository.ReplaceOneAsync(game, cancellationToken);

            await _mediator.Publish(new GameLoanedEvent(game.Id, game.Name, friend.Id, game.LoanedAt.Value));

            _logger.LogInformation("CreateGameLoanCommandHandler end of execution");

            return await Unit.Task;
        }
    }
}
