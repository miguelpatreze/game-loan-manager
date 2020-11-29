using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriends.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Friends.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, IEnumerable<GetFriendsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IFriendRepository _repository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger<GetFriendsQueryHandler> _logger;

        public GetFriendsQueryHandler(IMapper mapper,
            IFriendRepository repository,
            INotificationContext notificationContext,
            ILogger<GetFriendsQueryHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationContext = notificationContext;
            _logger = logger;
        }


        public async Task<IEnumerable<GetFriendsResponse>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetFriendsQueryHandler was called");

            var friends = await _repository.FindAsync(cancellationToken);

            if (!friends.Any())
            {
                _notificationContext.AddNotification("Nenhum Amigo Encontrado", "Não existe nenhum amigo no Banco de Dados :(");
                return default;
            }

            _logger.LogInformation("GetFriendsQueryHandler end of execution");

            return _mapper.Map<IEnumerable<GetFriendsResponse>>(friends);
        }
    }
}
