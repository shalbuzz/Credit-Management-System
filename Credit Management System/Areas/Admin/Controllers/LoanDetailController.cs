using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.LoanDetail;
using Credit_Management_System.ViewModels.LoanDetailVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin,Employee")]

    public class LoanDetailController : Controller
    {
        private readonly ILoanDetailService _loanDetailService;
        private readonly ILoanService _loanService;

        public LoanDetailController(ILoanDetailService loanDetailService, ILoanService loanService)
        {
            _loanDetailService = loanDetailService;
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var details = await _loanDetailService.GetAllWithLoansAsync();
            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var detail = await _loanDetailService.GetByIdWithLoansAndPaymentsAsync(id);
            if (detail == null) { TempData["Error"] = "LoanDetail not found."; return NotFound(); }
            return View(detail);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var loans = await _loanService.GetAvailableLoansForLoanDetailAsync();
            var model = new LoanDetailCreateVM
            {
                Loans = loans
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanDetailCreateVM loanDetail)
        {
            if (!ModelState.IsValid)
            {
                loanDetail.Loans = await _loanService.GetAvailableLoansForLoanDetailAsync();
                return View(loanDetail);
            }

            var loanExists = await _loanService.ExistsAsync(loanDetail.LoanId);
            if (!loanExists)
            {
                ModelState.AddModelError("LoanId", "Deleted.");
                loanDetail.Loans = await _loanService.GetAvailableLoansForLoanDetailAsync();
                return View(loanDetail);
            }

            await _loanDetailService.CreateWithLoansAsync(loanDetail);
            TempData["Success"] = "LoanDetail created successfully!";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var detail = await _loanDetailService.GetByIdVMWithLoansAndPaymentsAsync(id);
            if (detail == null)
            {
                TempData["Error"] = "LoanDetail not found.";
                return NotFound();
            }

            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanDetailUpdateVM loanDetail)
        {
            if (!ModelState.IsValid)
            {
                return View(loanDetail);
            }

            try
            {
                await _loanDetailService.UpdateWithLoansAsync(loanDetail);
                TempData["Success"] = "LoanDetail updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(loanDetail);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var detail = await _loanDetailService.GetByLoanIdAsync(id);
            if (detail == null){ TempData["Error"] = "LoanDetail not found."; return NotFound(); }
            return View(detail);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _loanDetailService.DeleteAsync(id);
            TempData["Success"] = "LoanDetail deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }

}
