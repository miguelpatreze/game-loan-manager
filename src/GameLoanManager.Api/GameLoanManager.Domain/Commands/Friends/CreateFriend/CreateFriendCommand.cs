using GameLoanManager.CrossCutting;
using MediatR;

namespace GameLoanManager.Domain.Commands.Friends.CreateFriend
{
    public class CreateFriendCommand : IRequest<string>
    {
        private CreateFriendCommand()
        {

        }
        public CreateFriendCommand(string name, string cellPhoneNumber)
        {
            Name = name;
            CellPhoneNumber = cellPhoneNumber.FormatCellPhoneNumber();
        }
        public string Name { get; set; }
        public string CellPhoneNumber { get; set; }
    }
}
