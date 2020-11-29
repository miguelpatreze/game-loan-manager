using GameLoanManager.Domain.Commands.Friends.DeleteFriendCommand;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Friends
{
    public class DeleteFriendCommandExample : IExamplesProvider<DeleteFriendCommand>
    {
        public DeleteFriendCommand GetExamples()
        {
            return new DeleteFriendCommand(ObjectId.GenerateNewId().ToString());
        }
    }
}
