using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class CategoryRepository :GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllExceptIdAsync(int id)
        {
            var categories = await _context.Categories
                .Where(c => c.Id != id)
                .Include(c => c.SubCategories)
                .Include(c => c.Products)
                .ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<Category>> GetAllWithRelationsAsync()
        {
              return await _context.Categories
             .Include(c => c.SubCategories)
             .Include(c => c.Products)
             .Where(c => !c.IsDeleted)
             .ToListAsync();
        }

        public async Task<Category> GetByIdWithRelationsAsync(int id)
        {
            return await _context.Categories
             .Include(c => c.SubCategories)
             .Include(c => c.Products)
             .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetByParentCategoryIdAsync(int parentCategoryId)
        {
           return await _context.Categories.Where(c => c.ParentCategoryId == parentCategoryId)
            .ToListAsync();
        }
    }
}
