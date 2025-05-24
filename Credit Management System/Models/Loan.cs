using Credit_Management_System.Enums;

namespace Credit_Management_System.Models
{
    public class Loan : BaseEntity
    {
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public LoanDetail LoanDetail { get; set; }
        public ICollection<LoanItem> LoanItems { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public LoanStatus StatusForLoan { get; set; } = LoanStatus.Pending;


    }

}
