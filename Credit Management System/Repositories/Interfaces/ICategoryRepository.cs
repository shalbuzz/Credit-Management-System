using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithRelationsAsync(); 
        Task<Category> GetByIdWithRelationsAsync(int id); 
        Task<IEnumerable<Category>> GetByParentCategoryIdAsync(int parentCategoryId);

        Task<IEnumerable<Category>> GetAllExceptIdAsync(int id);
    }
   
}
