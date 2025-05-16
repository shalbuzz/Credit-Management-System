using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Product;

namespace Credit_Management_System.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductVM, Product>
    {
        Task<IEnumerable<ProductVM>> GetAllWithCategoryAsync();
        Task<ProductVM?> GetByIdWithCategoryAsync(int id);
        Task<IEnumerable<ProductVM>> GetProductsByCategoryIdAsync(int categoryId);

        Task<ProductCreateVM> CreateWithCategoryAsync(ProductCreateVM productCreateVM);
        Task<ProductUpdateVM> UpdateWithCategoryAsync(ProductUpdateVM productUpdateVM);
        Task<ProductDetailsVM?> GetByIdVMWithCategoryAsync(int id);
        Task<ProductUpdateVM?> GetUpdateVMByIdAsync(int id);

        Task<ProductCreateVM> CreateWithImageAsync(ProductCreateVM vm);
        Task<ProductUpdateVM> UpdateWithImageAsync(ProductUpdateVM updateVM);

        Task<bool> DeleteWithImageAsync(int id);
    }
}
