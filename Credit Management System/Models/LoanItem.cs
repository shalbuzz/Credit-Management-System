namespace Credit_Management_System.Models
{
    public class LoanItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }

    }
    
}
