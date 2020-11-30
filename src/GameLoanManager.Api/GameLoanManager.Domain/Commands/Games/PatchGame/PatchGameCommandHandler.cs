using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Games.PatchGame
{
    public class PatchGameCommandHandler :
        IRequestHandler<PatchGameCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _repository;
        private readonly ILogger<PatchGameCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public PatchGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            ILogger<PatchGameCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(PatchGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"PatchGameCommandHandler was called Request.Id: {request.Id}");

            var game = await _repository.GetByIdAsync(request.Id);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.Id} não foi encontrado.");
                return await Unit.Task;
            }

            if (!string.IsNullOrWhiteSpace(request.Name) && !game.Name.Equals(request.Name, System.StringComparison.OrdinalIgnoreCase))
                game.SetName(request.Name);

            await _repository.ReplaceOneAsync(game, cancellationToken);

            _logger.LogInformation("PatchGameCommandHandler end of execution");

            return await Unit.Task;
        }

    }
}
