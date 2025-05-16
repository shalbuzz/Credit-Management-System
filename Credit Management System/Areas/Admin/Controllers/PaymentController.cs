using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Payment;
using Credit_Management_System.ViewModels.PaymentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoanService _loanService;

        public PaymentController(IPaymentService paymentService, ILoanService loanService)
        {
            _paymentService = paymentService;
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var payments = await _paymentService.GetAllWithLoansAsync();
            return View(payments);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _paymentService.GetByIdVMWithLoanAsync(id);
            if (payment == null)
            {
                TempData["error"] = "Payment not found.";
                return NotFound();
            }

            return View(payment);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var loans = await _loanService.GetAllAsync();
            var model = new PaymentCreateVM
            {
                Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentCreateVM payment)
        {
            if (!ModelState.IsValid)
            {
                var loans = await _loanService.GetAllAsync();
                payment.Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList();

                TempData["error"] = "Validation failed. Please correct the form.";
                return View(payment);
            }

            await _paymentService.CreateWithLoanAsync(payment);
            TempData["success"] = "Payment created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _paymentService.GetByIdVMUpdateVMWithLoanAsync(id);
            if (payment == null)
            {
                TempData["error"] = "Payment not found.";
                return NotFound();
            }

            var loans = await _loanService.GetAllAsync();
            ViewBag.Loans = loans.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.CustomerName
            }).ToList();

            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentUpdateVM payment)
        {
            if (!ModelState.IsValid)
            {
                var loans = await _loanService.GetAllAsync();
                payment.Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList();

                TempData["error"] = "Validation failed. Please correct the form.";
                return View(payment);
            }

            await _paymentService.UpdateWithLoanAsync(payment);
            TempData["success"] = "Payment updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null)
            {
                TempData["error"] = "Payment not found.";
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _paymentService.DeleteAsync(id);
            TempData["success"] = "Payment deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }

}
