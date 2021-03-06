﻿using GameLoanManager.Domain.Commands.Friends.PatchFriend;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Friends
{
    public class PatchFriendCommandExample : IExamplesProvider<PatchFriendCommand>
    {
        public PatchFriendCommand GetExamples()
        {
            return new PatchFriendCommand(ObjectId.GenerateNewId().ToString(), "Miguel Padoze", "(16) 98765-4321");
        }
    }
}
