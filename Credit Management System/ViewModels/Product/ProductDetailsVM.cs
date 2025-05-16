namespace Credit_Management_System.ViewModels.Product
{
    public class ProductDetailsVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

        public string CategoryName { get; set; }
    }
}
