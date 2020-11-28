using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriends.Responses;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Friends.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, IEnumerable<GetFriendsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;
        private readonly INotificationContext _notificationContext;

        public GetFriendsQueryHandler(IMapper mapper,
            IBaseRepository<Friend> repository,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationContext = notificationContext;
        }


        public async Task<IEnumerable<GetFriendsResponse>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var friends = await _repository.FindAsync(cancellationToken);

            if (!friends.Any())
            {
                _notificationContext.AddNotification("No Friends Found", "There are no Friends in the database :(");
                return default;
            }

            return _mapper.Map<IEnumerable<GetFriendsResponse>>(friends);
        }
    }
}
