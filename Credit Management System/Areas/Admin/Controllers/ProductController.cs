using System.Linq;
using Credit_Management_System.Data;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithCategoryAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdVMWithCategoryAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            if (product.Image != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(product.Image.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Image", "Only (.jpg, .jpeg, .png, .gif, .webp) files are allowed.");
                }

                if (product.Image.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "File size must be 5 MB or less.");
                }

                if (!product.Image.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("Image", "File must be an image.");
                }
            }

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                product.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(product);
            }

            await _productService.CreateWithImageAsync(product);
            TempData["Success"] = "Product created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetUpdateVMByIdAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductUpdateVM product)
        {
            if (product.Image != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var extension = Path.GetExtension(product.Image.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Image", "Only (.jpg, .jpeg, .png, .gif, .webp) files are allowed.");
                }

                if (product.Image.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "File size must be 5 MB or less.");
                }

                if (!product.Image.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("Image", "File must be an image.");
                }
            }

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                product.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(product);
            }

            await _productService.UpdateWithImageAsync(product);
            TempData["Success"] = "Product updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteWithImageAsync(id);
            TempData["Success"] = "Product deleted.";
            return RedirectToAction(nameof(Index));
        }
    }

}
