using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Games.DeleteGame
{
    public class DeleteGameCommandHandler :
        IRequestHandler<DeleteGameCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _repository;
        private readonly ILogger<DeleteGameCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public DeleteGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            ILogger<DeleteGameCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DeleteGameCommandHandler was called Request.Id: {request.Id}");

            var game = await _repository.GetByIdAsync(request.Id);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.Id} não foi encontrado.");
                return await Unit.Task;
            }

            await _repository.DeleteOneAsync(game, cancellationToken);

            _logger.LogInformation("DeleteGameCommandHandler end of execution");
           
            return await Unit.Task;
        }
    }
}
