using System;

namespace GameLoanManager.Domain.Entities
{
    public class Game : EntityBase
    {
        public Game(string name)
            : base()
        {
            Name = name;
            NormalizedName = name?.ToLowerInvariant();
        }
        public Game(string id, string name)
            : base(id)
        {
            Name = name;
            NormalizedName = name?.ToLowerInvariant();
        }

        private string _name;
        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name is Required to create a new Game.");

                _name = value;
            }
        }
        public string NormalizedName { get; private set; }


        public void SetName(string name) => Name = name;
    }
}
