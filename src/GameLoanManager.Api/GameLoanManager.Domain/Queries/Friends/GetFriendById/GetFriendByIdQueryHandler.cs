using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById
{
    public class GetFriendByIdQueryHandler : IRequestHandler<GetFriendByIdQuery, GetFriendByIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<GetFriendByIdQueryHandler> _logger;

        public GetFriendByIdQueryHandler(IMapper mapper,
            IBaseRepository<Friend> repository,
            INotificationContext notificationContext,
            ILogger<GetFriendByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        public async Task<GetFriendByIdResponse> Handle(GetFriendByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetFriendByIdQueryHandler was called");
            
            var friend = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (friend == null)
            {
                _notificationContext.AddNotification("Amigo não encontrado", $"O amigo com o id:{request.Id} não foi encontrado.");
                return default;
            }

            _logger.LogInformation("GetFriendByIdQueryHandler end of execution");
            
            return _mapper.Map<GetFriendByIdResponse>(friend);
        }
    }
}
