using GameLoanManager.Domain.Queries.Games.GetGames.Responses;
using MediatR;
using System.Collections.Generic;

namespace GameLoanManager.Domain.Queries.Games.GetGames
{
    public class GetGamesQuery : IRequest<IEnumerable<GetGamesResponse>>
    {
        
    }
}
