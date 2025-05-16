using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Credit_Management_System.Models
{
    public class LoanDetail : BaseEntity
    {
        public decimal Amount { get; set; }
        public decimal CurrentDebt { get; set; } 
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } 
        [Key]
        public int LoanId { get; set; }
        public Loan Loan { get; set; } 

    }
   
}
