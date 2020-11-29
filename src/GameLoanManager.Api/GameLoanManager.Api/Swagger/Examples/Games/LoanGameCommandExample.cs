using GameLoanManager.Domain.Commands.Games.LoanGame;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Games
{
    public class LoanGameCommandExample : IExamplesProvider<LoanGameCommand>
    {
        public LoanGameCommand GetExamples()
        {
            return new LoanGameCommand(
                ObjectId.GenerateNewId().ToString(),
                ObjectId.GenerateNewId().ToString());
        }
    }
}
