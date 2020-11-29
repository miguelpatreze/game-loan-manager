using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Games.ReturnGame
{
    public class ReturnGameCommandHandler :
        IRequestHandler<ReturnGameCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly ILogger<ReturnGameCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public ReturnGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            ILogger<ReturnGameCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _gameRepository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(ReturnGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ReturnGameCommandHandler was called Request.GameId: {request.GameId}");

            var game = await _gameRepository.GetByIdAsync(request.GameId);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.GameId} não foi encontrado.");
                await Unit.Task;
            }

            game.ReturnGame();
            await _gameRepository.ReplaceOneAsync(game, cancellationToken);

            _logger.LogInformation("ReturnGameCommandHandler end of execution");

            return await Unit.Task;
        }
    }
}
