using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class MerchantRepository :GenericRepository<Merchant>, IMerchantRepository
    {
        private readonly AppDbContext _context;
        public MerchantRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Merchant>> GetAllWithBranchesAsync()
        {
            return await _context.Merchants
                 .Include(c => c.Branches)
                 .Where(c => !c.IsDeleted)  
                 .ToListAsync();
        }

        public async Task<Merchant?> GetByIdWithBranchesAsync(int id)
        {
            return await _context.Merchants
                 .Include(c => c.Branches)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
