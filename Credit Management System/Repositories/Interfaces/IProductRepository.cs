using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
        public interface IProductRepository : IGenericRepository<Product>
        {
            Task<IEnumerable<Product>> GetAllWithCategoryAsync();
            Task<Product?> GetByIdWithCategoryAsync(int id);
            Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        }
}
