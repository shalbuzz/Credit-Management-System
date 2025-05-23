using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.ViewModels.LoanDetail
{
    public class LoanDetailCreateVM
    {
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }


        [Required(ErrorMessage = "Interest rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Interest rate must be greater than zero.")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Current debt is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Current debt must be greater than zero.")]
        public decimal CurrentDebt { get; set; }

        [Required(ErrorMessage = "Loan selection is required.")]
        public int LoanId { get; set; }

        public List<SelectListItem> Loans { get; set; } = new List<SelectListItem>();
    }
}
