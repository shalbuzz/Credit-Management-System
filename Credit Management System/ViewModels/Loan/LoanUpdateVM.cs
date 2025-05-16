using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.ViewModels.Loan
{
    public class LoanUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Interest rate is required.")]
        [Range(0.01, 100, ErrorMessage = "Interest rate must be between 0.01 and 100.")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Duration in months is required.")]
        [Range(1, 120, ErrorMessage = "Duration must be between 1 and 120 months.")]
        public int DurationInMonths { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        [Compare("StartDate", ErrorMessage = "End date must be later than the start date.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Customer selection is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Employee selection is required.")]
        public int EmployeeId { get; set; }

        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
    }
}
