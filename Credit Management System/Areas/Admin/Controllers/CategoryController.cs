using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin,Employee")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllWithRelationsAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdVMWithRelationsAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found.";
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var parentCategories = await _categoryService.GetAllAsync();
            var model = new CategoryCreateVM
            {
                ParentCategories = parentCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                var parentCategories = await _categoryService.GetAllAsync();
                category.ParentCategories = parentCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                TempData["Error"] = "Please correct the errors in the form.";
                return View(category);
            }

            await _categoryService.CreateWithRelationsAsync(category);
            TempData["Success"] = "Category created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdUpdateVMWithRelationsAsync(id); 
            if (category == null) {
                TempData["Error"] = "Category not found."; 
                return NotFound(); }

            var parentCategories = await _categoryService.GetAllExceptIdAsync(id);
            ViewBag.ParentCategories = parentCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryUpdateVM category)
        {
            if (!ModelState.IsValid)
            {
                var parentCategories = await _categoryService.GetAllExceptIdAsync(category.Id);
                category.ParentCategories = parentCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
                TempData["Error"] = "Please correct the errors in the form.";
                return View(category);
            }

            await _categoryService.UpdateWithRelationsAsync(category);
            TempData["Success"] = "Category updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) { TempData["Error"] = "Category not found."; return NotFound(); }
            return View(category);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            TempData["Success"] = "Category deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Subcategories(int parentCategoryId)
        {
            var subcategories = await _categoryService.GetByParentCategoryIdAsync(parentCategoryId);
            return View(subcategories);
        }
    }

}
