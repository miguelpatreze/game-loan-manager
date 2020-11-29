using AutoMapper;
using GameLoanManager.Domain.Entities;

namespace GameLoanManager.Domain.Commands.Games.CreateGame
{
    public class CreateGameCommandProfile : Profile
    {
        public CreateGameCommandProfile()
        {
            CreateMap<CreateGameCommand, Game>();
        }
    }
}
