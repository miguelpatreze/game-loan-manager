using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Friends.GetFriendById.Responses;

namespace GameLoanManager.Domain.Queries.Friends.GetFriendById
{
    public class GetFriendByIdQueryProfile : Profile
    {
        public GetFriendByIdQueryProfile()
        {
            CreateMap<Friend, GetFriendByIdResponse>();
        }
    }
}
