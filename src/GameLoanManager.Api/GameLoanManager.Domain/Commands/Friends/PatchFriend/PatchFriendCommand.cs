using GameLoanManager.CrossCutting;
using MediatR;

namespace GameLoanManager.Domain.Commands.Friends.PatchFriend
{
    public class PatchFriendCommand : IRequest
    {
        private PatchFriendCommand()
        {

        }
        public PatchFriendCommand(string id, string name, string cellPhoneNumber)
        {
            Id = id;
            Name = name;
            CellPhoneNumber = cellPhoneNumber.FormatCellPhoneNumber();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string CellPhoneNumber { get; set; }
    }
}
