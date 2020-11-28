using GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses;
using MediatR;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById
{
    public class GetFriendByIdQuery : IRequest<GetFriendByIdResponse>
    {
        public GetFriendByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
