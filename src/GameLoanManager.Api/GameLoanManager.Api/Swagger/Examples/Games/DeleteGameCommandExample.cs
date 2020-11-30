using GameLoanManager.Domain.Commands.Games.DeleteGame;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Games
{
    public class DeleteGameCommandExample : IExamplesProvider<DeleteGameCommand>
    {
        public DeleteGameCommand GetExamples()
        {
            return new DeleteGameCommand(ObjectId.GenerateNewId().ToString());
        }
    }
}
