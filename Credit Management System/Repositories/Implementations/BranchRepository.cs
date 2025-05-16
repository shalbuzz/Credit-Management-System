using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        private readonly AppDbContext _context;
        public BranchRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Branch>> GetBranchesByMerchantIdAsync(int merchantId)
        {
            return await _context.Branches
            .Where(b => b.MerchantId == merchantId)
             .Where(c => !c.IsDeleted)
            .ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetBranchesWithMerchantAsync()
        {
              return await _context.Branches
             .Include(b => b.Merchant)
              .Where(c => !c.IsDeleted)
             .ToListAsync();
        }

        public async Task<Branch?> GetBranchWithEmployeesAsync(int id)
        {
            return await _context.Branches
           .Include(b => b.Employees)
           .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
