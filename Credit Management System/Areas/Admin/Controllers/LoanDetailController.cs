using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.LoanDetail;
using Credit_Management_System.ViewModels.LoanDetailVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var loans = await _loanService.GetAllAsync();
            var model = new LoanDetailCreateVM
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
        public async Task<IActionResult> Create(LoanDetailCreateVM loanDetail)
        {
            if (!ModelState.IsValid)
            {
                var loans = await _loanService.GetAllAsync();
                loanDetail.Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList();

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
            if (detail == null) { TempData["Error"] = "LoanDetail not found."; return NotFound(); }
            var loans = await _loanService.GetAllAsync();
            var model = new LoanDetailUpdateVM
            {
                Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList()
            };
            
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanDetailUpdateVM loanDetail)
        {
            if (!ModelState.IsValid)
            {
                var loans = await _loanService.GetAllAsync();
                loanDetail.Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList();

                return View(loanDetail);
            }
                

            await _loanDetailService.UpdateWithLoansAsync(loanDetail);
            TempData["Success"] = "LoanDetail updated successfully!";
            return RedirectToAction(nameof(Index));
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
