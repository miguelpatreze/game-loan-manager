using GameLoanManager.Domain.Reponses;
using MediatR;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById
{
    public class GetFriendByIdQuery : IRequest<Response>
    {
        public GetFriendByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
