using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Category;

namespace Credit_Management_System.Services.Interfaces
{
    public interface ICategoryService : IGenericService<CategoryVM,Category>
    {
        Task<IEnumerable<CategoryVM>> GetAllWithRelationsAsync();
        Task<CategoryVM?> GetByIdWithRelationsAsync(int id);
        Task<IEnumerable<CategoryVM>> GetByParentCategoryIdAsync(int parentCategoryId);

        Task<IEnumerable<CategoryVM>> GetAllExceptIdAsync(int id);
        Task<CategoryCreateVM> CreateWithRelationsAsync(CategoryCreateVM categoryCreateVM);
        Task<CategoryUpdateVM> UpdateWithRelationsAsync(CategoryUpdateVM categoryUpdateVM);
        Task<CategoryDetailsVM?> GetByIdVMWithRelationsAsync(int id);
        Task<CategoryUpdateVM?> GetByIdUpdateVMWithRelationsAsync(int id);

    }
}
