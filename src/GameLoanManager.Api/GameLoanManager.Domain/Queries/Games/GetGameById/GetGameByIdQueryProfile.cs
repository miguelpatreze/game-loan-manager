using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Queries.Games.GetGameById.Responses;

namespace GameLoanManager.Domain.Queries.Games.GetGameById
{
    public class GetGameByIdQueryProfile : Profile
    {
        public GetGameByIdQueryProfile()
        {
            CreateMap<Game, GetGameByIdResponse>();
        }
    }
}
