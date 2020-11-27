namespace GameLoanManager.Domain.Entities
{
    public class Friend : EntityBase
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string CellPhoneNumber { get; set; }
    }
}
