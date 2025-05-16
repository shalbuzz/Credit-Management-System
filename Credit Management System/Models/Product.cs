namespace Credit_Management_System.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<LoanItem> LoanItems { get; set; }

    }
}
