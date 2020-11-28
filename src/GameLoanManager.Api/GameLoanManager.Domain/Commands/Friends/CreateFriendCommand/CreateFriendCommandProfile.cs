using AutoMapper;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Commands.Friends.CreateFriendCommand
{
    public class CreateFriendCommandProfile : Profile
    {
        public CreateFriendCommandProfile()
        {
            CreateMap<CreateFriendCommand, Friend>();
        }
    }
}
