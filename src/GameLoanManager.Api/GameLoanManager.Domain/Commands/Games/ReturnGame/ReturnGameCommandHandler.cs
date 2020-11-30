using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Events.GameReturned;
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
        private readonly IBaseRepository<Game> _repository;
        private readonly ILogger<ReturnGameCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;
        private readonly IMediator _mediator;

        public ReturnGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            ILogger<ReturnGameCommandHandler> logger,
            INotificationContext notificationContext,
            IMediator mediator)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ReturnGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ReturnGameCommandHandler was called Request.GameId: {request.GameId}");

            var game = await _repository.GetByIdAsync(request.GameId);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.GameId} não foi encontrado.");
                return await Unit.Task;
            }

            game.ReturnGame();
            await _repository.ReplaceOneAsync(game, cancellationToken);

            await _mediator.Publish(new GameReturnedEvent(game.Id));

            _logger.LogInformation("ReturnGameCommandHandler end of execution");

            return await Unit.Task;
        }
    }
}
