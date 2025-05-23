namespace Credit_Management_System.ViewModels.LoanDetailVM
{
    public class LoanDetailVM
    {
        public int Id { get; set; } 
        public int LoanId { get; set; } 
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } 
        public decimal CurrentDebt { get; set; } 

    }
}
