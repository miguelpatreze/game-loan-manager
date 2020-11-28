using AutoMapper;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Commands.Friends
{
    public class CreateFriendCommandProfile : Profile
    {
        public CreateFriendCommandProfile()
        {
            CreateMap<CreateFriendCommand, Friend>();
                //.ForMember(f => f.NormalizedName, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }

    }
}
