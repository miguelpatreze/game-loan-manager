using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Games.GetGameById.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Games.GetGameById
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GetGameByIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _repository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<GetGameByIdQueryHandler> _logger;

        public GetGameByIdQueryHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            INotificationContext notificationContext,
            ILogger<GetGameByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task<GetGameByIdResponse> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetGameByIdQueryHandler was called");

            var game = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.Id} não foi encontrado.");
                return default;
            }

            _logger.LogInformation("GetGameByIdQueryHandler end of execution");

            return _mapper.Map<GetGameByIdResponse>(game);
        }
    }
}
