using AutoMapper;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Commands.Games.CreateGameCommand
{
    public class CreateGameCommandProfile : Profile
    {
        public CreateGameCommandProfile()
        {
            CreateMap<CreateGameCommand, Game>();
        }
    }
}
