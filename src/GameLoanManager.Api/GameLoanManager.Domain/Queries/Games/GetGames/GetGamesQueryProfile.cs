using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Games.GetGames.Responses;

namespace GameLoanManager.Domain.Queries.Games.GetGames
{
    public class GetGamesQueryProfile : Profile
    {
        public GetGamesQueryProfile()
        {
            CreateMap<Game, GetGamesResponse>();
        }
    }
}
