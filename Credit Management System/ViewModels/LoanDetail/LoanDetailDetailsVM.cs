namespace Credit_Management_System.ViewModels.LoanDetail
{
    public class LoanDetailDetailsVM
    {
        public int LoanId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } 
        public decimal CurrentDebt { get; set; } 
    }
}
