using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.ViewModels.LoanItem
{
    public class LoanItemCreateVM
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Product selection is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Loan selection is required.")]
        public int LoanId { get; set; }

        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Loans { get; set; } = new List<SelectListItem>();
    }
}
