using GameLoanManager.CrossCutting;
using MediatR;

namespace GameLoanManager.Domain.Commands.Friends
{
    public class CreateFriendCommand : IRequest<Unit>
    {
        public CreateFriendCommand()
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
