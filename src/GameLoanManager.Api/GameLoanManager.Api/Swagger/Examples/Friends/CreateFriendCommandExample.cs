using GameLoanManager.Domain.Commands.Friends.CreateFriend;
using Swashbuckle.AspNetCore.Filters;

namespace GameLoanManager.Api.Swagger.Examples.Friends
{
    public class CreateFriendCommandExample : IExamplesProvider<CreateFriendCommand>
    {
        public CreateFriendCommand GetExamples()
        {
            return new CreateFriendCommand("Miguel Patreze", "(16) 91234-5678");
        }
    }
}
