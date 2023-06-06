namespace AdminCheckWeb.Models
{
    public class FinancialForecast
    {

        public string? Name { get; set; }

        public int Account_Id { get; set; }

        public string? Account_Type { get; set; }

        public float Balance { get; set; }

        public float Interest { get; set; }

        public float BalanceAfterAYear { get; set; }

        public float TotalBalance { get; set; }



    }
}
