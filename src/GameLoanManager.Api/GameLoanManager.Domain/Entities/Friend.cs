using GameLoanManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace GameLoanManager.Domain.Entities
{
    public class Friend : EntityBase
    {
        public Friend(string name, string cellPhoneNumber)
            : base()
        {
            Name = name;
            NormalizedName = name?.ToLowerInvariant();
            CellPhoneNumber = cellPhoneNumber;
            LoanedGames = new List<LoanedGame>();
        }
        public Friend(string id, string name, string cellPhoneNumber)
            : base(id)
        {
            Name = name;
            NormalizedName = name?.ToLowerInvariant();
            CellPhoneNumber = cellPhoneNumber;
            LoanedGames = new List<LoanedGame>();
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
        public IList<LoanedGame> LoanedGames { get; private set; }

        public void SetName(string name) => Name = name;
        public void SetCellPhoneNumber(string cellPhoneNumber) => CellPhoneNumber = cellPhoneNumber;
        public void LoanGame(string gameId, string gameName, DateTime loanedAt)
        {
            if (LoanedGames is null)
                LoanedGames = new List<LoanedGame>();

            LoanedGames.Add(new LoanedGame(gameId, gameName, loanedAt));
        }
    }
}
