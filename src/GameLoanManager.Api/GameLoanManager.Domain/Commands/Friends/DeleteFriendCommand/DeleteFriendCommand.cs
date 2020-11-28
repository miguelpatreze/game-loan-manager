using MediatR;

namespace GameLoanManager.Domain.Commands.Friends.DeleteFriendCommand
{
    public class DeleteFriendCommand : IRequest
    {
        public DeleteFriendCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
