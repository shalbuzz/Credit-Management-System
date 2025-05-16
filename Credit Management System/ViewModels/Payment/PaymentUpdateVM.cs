using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.ViewModels.Payment
{
    public class PaymentUpdateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        [StringLength(50, ErrorMessage = "Payment Method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Reference Number is required.")]
        [StringLength(100, ErrorMessage = "Reference Number cannot exceed 100 characters.")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Loan is required.")]
        public int LoanId { get; set; }

        public List<SelectListItem> Loans { get; set; } = new List<SelectListItem>();
    }
}
