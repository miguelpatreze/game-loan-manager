using AutoMapper;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses;
using GameLoanManager.Domain.Reponses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById
{
    public class GetFriendByIdQueryHandler : IRequestHandler<GetFriendByIdQuery, Response>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;

        public GetFriendByIdQueryHandler(IMapper mapper,
            IBaseRepository<Friend> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Response> Handle(GetFriendByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var friend = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (friend == null)
            {
                response.AddNotification("Friend not Found", $"The Friend with id:{request.Id} was not found.", Notifications.NotificationType.NotFound);
                return response;
            }

            var payload = _mapper.Map<GetFriendByIdResponse>(friend);

            return new Response(payload, null);
        }
    }
}
