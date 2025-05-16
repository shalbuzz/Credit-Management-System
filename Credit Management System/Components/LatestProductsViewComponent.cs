using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Credit_Management_System.Components
{
    public class LatestProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public LatestProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 5)
        {
            var allProducts = await _productService.GetAllWithCategoryAsync();

            if (allProducts == null || !allProducts.Any())
            {
                return View(new List<ProductVM>());
            }

            var latest = allProducts
                .OrderByDescending(p => p.Id)
                .Take(count)
                .ToList();

            return View(latest);
        }
    }

}
