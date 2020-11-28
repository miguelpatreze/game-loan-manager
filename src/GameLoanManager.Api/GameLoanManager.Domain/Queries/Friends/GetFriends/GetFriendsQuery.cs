using GameLoanManager.Domain.Queries.Friends.GetFriends.Responses;
using MediatR;
using System.Collections.Generic;

namespace GameLoanManager.Domain.Queries.Friends.GetFriends
{
    public class GetFriendsQuery : IRequest<IEnumerable<GetFriendsResponse>>
    {
        
    }
}
