using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriends.Responses;

namespace GameLoanManager.Domain.Queries.Friends.GetFriends
{
    public class GetFriendsQueryProfile : Profile
    {
        public GetFriendsQueryProfile()
        {
            CreateMap<Friend, GetFriendsResponse>();
        }
    }
}
