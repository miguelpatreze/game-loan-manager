using AutoMapper;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriends.Responses;
using GameLoanManager.Domain.Reponses;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Friends.GetFriends
{
    public class GetFriendsQueryHandler : IRequestHandler<GetFriendsQuery, Response>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;

        public GetFriendsQueryHandler(IMapper mapper,
            IBaseRepository<Friend> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public async Task<Response> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var friends = await _repository.FindAsync(cancellationToken);

            if (!friends.Any())
            {
                response.AddNotification("No Friends Found", "There are no Friends in the database :(", Notifications.NotificationType.NotFound);
                return response;
            }

            var payload = _mapper.Map<List<GetFriendsResponse>>(friends);

            return new Response(payload, null);
        }
    }
}
