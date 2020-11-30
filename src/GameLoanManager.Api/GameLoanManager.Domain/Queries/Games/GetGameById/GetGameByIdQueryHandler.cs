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
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<GetGameByIdQueryHandler> _logger;

        public GetGameByIdQueryHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            INotificationContext notificationContext,
            ILogger<GetGameByIdQueryHandler> logger,
            IFriendRepository friendRepository)
        {
            _mapper = mapper;
            _gameRepository = repository;
            _notificationContext = notificationContext;
            _logger = logger;
            _friendRepository = friendRepository;
        }

        public async Task<GetGameByIdResponse> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetGameByIdQueryHandler was called");

            var game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken);

            if (game == null)
            {
                _notificationContext.AddNotification("Jogo não encontrado", $"O jogo com o id:{request.Id} não foi encontrado.");
                return default;
            }

            var response = _mapper.Map<GetGameByIdResponse>(game);

            if (game.Loaned)
            {
                var friend = await _friendRepository.GetByGameIdAsync(game.Id, cancellationToken);
                response.LoanedTo = friend?.Name;
            }

            _logger.LogInformation("GetGameByIdQueryHandler end of execution");

            return response;
        }
    }
}
