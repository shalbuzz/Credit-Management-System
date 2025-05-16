using Credit_Management_System.Data;
using Credit_Management_System.Services.Implementations;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Branch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IMerchantService _merchantService;

        public BranchController(IBranchService branchService, IMerchantService merchantService)
        {
            _branchService = branchService;
            _merchantService = merchantService;
        }

       
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetBranchesWithMerchantAsync();
            return View(branches);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var branch = await _branchService.CreateBranchDetailsAsync(id);
            if (branch == null) { TempData["Error"] = "Branch not found."; return NotFound(); }
            return View(branch);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var merchants = await _merchantService.GetAllAsync();

            
            var model = new BranchCreateVM
            {
                Merchants = merchants.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };

            return View(model);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchCreateVM branch)
        {
            if (!ModelState.IsValid)
            {
                
                var merchants = await _merchantService.GetAllAsync();
                branch.Merchants = merchants.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();
                TempData["Error"] = "Please correct the errors in the form.";
                return View(branch);
            }

          
            var createdBranch = await _branchService.CreateBranchAsync(branch);
            TempData["Success"] = "Branch created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           
            var branch = await _branchService.GetByIdVMWithMerchantAsync(id);
            if (branch == null)
            {
                TempData["Error"] = "Branch not found.";
                return NotFound(); 
            }

            var merchants = await _merchantService.GetAllAsync();
            ViewBag.Merchants = merchants.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),  
                Text = m.Name             
            }).ToList();

            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BranchUpdateVM branch)
        {
            if (!ModelState.IsValid)
            {
                var merchants = await _merchantService.GetAllAsync();
                ViewBag.Merchants = merchants.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();
                TempData["Error"] = "Please correct the errors in the form.";
                return View(branch);
            }

            await _branchService.UpdateBranchAsync(branch);
            TempData["Success"] = "Branch updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) { TempData["Error"] = "Branch not found."; return NotFound(); }
            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _branchService.DeleteAsync(id);
            TempData["Success"] = "Branch deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByMerchant(int merchantId)
        {
            var branches = await _branchService.GetBranchesByMerchantIdAsync(merchantId);
            return View(branches); 
        }
    }

}
