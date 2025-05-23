namespace Credit_Management_System.ViewModels.Payment
{
    public class PaymentDetailsVM
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } 
        public string ReferenceNumber { get; set; } 
        public int LoanId { get; set; }
    }
}
