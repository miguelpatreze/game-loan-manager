using System;

namespace GameLoanManager.Domain.Entities
{
    public class Friend : EntityBase
    {
        public Friend(string name, string cellPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is Required to create a new Friend.");

            Name = name;
            NormalizedName = name?.ToLowerInvariant();
            CellPhoneNumber = cellPhoneNumber;
        }
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }
        public string CellPhoneNumber { get; private set; }
    }
}
