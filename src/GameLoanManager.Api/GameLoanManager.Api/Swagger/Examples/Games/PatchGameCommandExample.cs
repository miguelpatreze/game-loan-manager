﻿using GameLoanManager.Domain.Commands.Games.PatchGameCommand;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Games
{
    public class PatchGameCommandExample : IExamplesProvider<PatchGameCommand>
    {
        public PatchGameCommand GetExamples()
        {
            return new PatchGameCommand(ObjectId.GenerateNewId().ToString(), "Dark Souls 2");
        }
    }
}
