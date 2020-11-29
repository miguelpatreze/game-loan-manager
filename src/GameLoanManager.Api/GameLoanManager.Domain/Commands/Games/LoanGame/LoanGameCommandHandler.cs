using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
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
        private readonly IBaseRepository<Friend> _friendRepository;
        private readonly ILogger<LoanGameCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public LoanGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            IBaseRepository<Friend> friendRepository,
            ILogger<LoanGameCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _gameRepository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
            _friendRepository = friendRepository;
        }

        public async Task<Unit> Handle(LoanGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateGameLoanCommandHandler was called Request.GameId: {request.GameId} Request.UserId: {request.FriendId}");

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


            _logger.LogInformation("CreateGameLoanCommandHandler end of execution");

            return await Unit.Task;
        }
    }
}
