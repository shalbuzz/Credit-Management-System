namespace Credit_Management_System.ViewModels.Loan
{
    public class LoanDetailsVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }

    }
}
