namespace Credit_Management_System.ViewModels.LoanItemVM
{
    public class LoanItemVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; }
    }
}
