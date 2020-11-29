using GameLoanManager.Domain.Commands.Friends.PatchFriendCommand;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Friends
{
    public class PatchFriendCommandExample : IExamplesProvider<PatchFriendCommand>
    {
        public PatchFriendCommand GetExamples()
        {
            return new PatchFriendCommand(new ObjectId().ToString(), "Miguel Padoze", "(16) 98765-4321");
        }
    }
}
