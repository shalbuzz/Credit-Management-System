namespace Credit_Management_System.Models
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }


        public ICollection<Loan> LoansHandled { get; set; } = new List<Loan>();
    }

}
