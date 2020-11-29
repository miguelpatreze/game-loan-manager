using GameLoanManager.Domain.Commands.Games.CreateGameCommand;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Games
{
    public class CreateGameCommandExample : IExamplesProvider<CreateGameCommand>
    {
        public CreateGameCommand GetExamples()
        {
            return new CreateGameCommand("Dark Souls 3");
        }
    }
}
