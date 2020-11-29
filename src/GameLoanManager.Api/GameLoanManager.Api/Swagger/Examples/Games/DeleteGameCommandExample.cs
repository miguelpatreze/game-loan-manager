using GameLoanManager.Domain.Commands.Games.DeleteGameCommand;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Games
{
    public class DeleteGameCommandExample : IExamplesProvider<DeleteGameCommand>
    {
        public DeleteGameCommand GetExamples()
        {
            return new DeleteGameCommand(ObjectId.GenerateNewId().ToString().ToString());
        }
    }
}
