using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Merchant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class MerchantController : Controller
    {
        private readonly IMerchantService _merchantService;

        public MerchantController(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var merchants = await _merchantService.GetAllWithBranchesAsync();
            return View(merchants);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var merchant = await _merchantService.GetByIdWithBranchAsync(id);
            if (merchant == null)
            {
                TempData["error"] = "Merchant not found.";
                return NotFound();
            }
            return View(merchant);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MerchantCreateVM merchant)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Validation error. Please correct the form.";
                return View(merchant);
            }

            await _merchantService.CreateWithBranchAsync(merchant);
            TempData["success"] = "Merchant created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var merchant = await _merchantService.GetByIdVMUpdateVMWithBranchAsync(id);
            if (merchant == null)
            {
                TempData["error"] = "Merchant not found.";
                return NotFound();
            }
            return View(merchant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MerchantUpdateVM merchant)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Validation error. Please correct the form.";
                return View(merchant);
            }

            await _merchantService.UpdateWithBranchAsync(merchant);
            TempData["success"] = "Merchant updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var merchant = await _merchantService.GetByIdAsync(id);
            if (merchant == null)
            {
                TempData["error"] = "Merchant not found.";
                return NotFound();
            }
            return View(merchant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _merchantService.DeleteAsync(id);
            TempData["success"] = "Merchant deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }

}
