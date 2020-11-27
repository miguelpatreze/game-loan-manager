using GameLoanManager.CrossCutting;
using GameLoanManager.Domain.Reponses;
using MediatR;

namespace GameLoanManager.Domain.Commands.Friends
{
    public class CreateFriendCommand : IRequest<Response>
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
