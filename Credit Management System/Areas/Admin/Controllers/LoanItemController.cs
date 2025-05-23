using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.LoanItem;
using Credit_Management_System.ViewModels.LoanItemVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin,Employee")]

    public class LoanItemController : Controller
    {
        private readonly ILoanItemService _loanItemService;
        private readonly ILoanService _loanService;
        private readonly IProductService _productService;

        public LoanItemController(ILoanItemService loanItemService, ILoanService loanService, IProductService productService)
        {
            _loanItemService = loanItemService;
            _loanService = loanService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _loanItemService.GetAllWithLoanAndProductAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _loanItemService.GetByIdWithLoanAndProductAsync(id);
            if (item == null) { TempData["Error"] = "LoanItem not found."; return NotFound(); }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var loans = await _loanService.GetLoansWithCustomerAndEmployeeAsync();
            var products = await _productService.GetAllAsync();
            var model = new LoanItemCreateVM
            {
                Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList(),
                Products = products.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanItemCreateVM loanItem)
        {
            if (!ModelState.IsValid)
            {
                var loans = await _loanService.GetLoansWithCustomerAndEmployeeAsync();
                var products = await _productService.GetAllAsync();
                loanItem.Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList();
                loanItem.Products = products.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();

               
                return View(loanItem);
            }
                

            await _loanItemService.CreateWithLoanAndProductAsync(loanItem);
            TempData["Success"] = "LoanItem created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _loanItemService.GetByIdVMWithLoanAndProductAsync(id);
            if (item == null) return NotFound();

            var loans = await _loanService.GetLoansWithCustomerAndEmployeeAsync();
            var products = await _productService.GetAllAsync();

            item.Loans = loans.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.CustomerName
            }).ToList();

            item.Products = products.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();

            return View(item); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanItemUpdateVM loanItem)
        {
            if (!ModelState.IsValid)
            {
                var loans = await _loanService.GetLoansWithCustomerAndEmployeeAsync();
                var products = await _productService.GetAllAsync();

                loanItem.Loans = loans.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CustomerName
                }).ToList();

                loanItem.Products = products.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();

                return View(loanItem); 
            }


            await _loanItemService.UpdateWithLoanAndProductAsync(loanItem);
            TempData["Success"] = "LoanItem updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _loanItemService.GetLoanItemWithProductAsync(id);
            if (item == null) { TempData["Error"] = "LoanItem not found."; return NotFound(); }
            return View(item);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _loanItemService.DeleteAsync(id);
            TempData["Success"] = "LoanItem deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }

}
