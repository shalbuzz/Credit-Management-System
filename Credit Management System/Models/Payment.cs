namespace Credit_Management_System.Models
{
    public class Payment : BaseEntity
    {
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } 
        public string ReferenceNumber { get; set; } 

        public int LoanId { get; set; }
        public Loan Loan { get; set; } 

    }
    
}
