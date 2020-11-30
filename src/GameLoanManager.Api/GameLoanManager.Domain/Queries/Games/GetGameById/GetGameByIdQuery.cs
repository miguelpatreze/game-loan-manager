using GameLoanManager.Domain.Queries.Games.GetGameById.Responses;
using MediatR;

namespace GameLoanManager.Domain.Queries.Games.GetGameById
{
    public class GetGameByIdQuery : IRequest<GetGameByIdResponse>
    {
        public GetGameByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
