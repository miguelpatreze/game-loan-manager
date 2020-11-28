using System;

namespace GameLoanManager.Domain.Entities
{
    public class Friend : EntityBase
    {
        public Friend(string name, string cellPhoneNumber)
        {
            Name = name;
            NormalizedName = name?.ToLowerInvariant();
            CellPhoneNumber = cellPhoneNumber;
        }
        private string _name;
        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name is Required to create a new Friend.");

                _name = value;
            }
        }
        public string NormalizedName { get; private set; }
        public string CellPhoneNumber { get; private set; }


        public void SetName(string name) => Name = name;
        public void SetCellPhoneNumber(string cellPhoneNumber) => CellPhoneNumber = cellPhoneNumber;
    }
}
