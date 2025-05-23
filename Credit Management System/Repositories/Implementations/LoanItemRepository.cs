using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class LoanItemRepository :GenericRepository<LoanItem>, ILoanItemRepository
    {
        private readonly AppDbContext _context;
        public LoanItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanItem>> GetAllWithLoanAndProductAsync()
        {
            return await _context.LoanItems
                .Include(x => x.Loan)
                    .ThenInclude(l => l.Customer)
                .Include(x => x.Product)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanItem>> GetLoanItemsWithProductAsync(int loanId)
        {
           return await _context.LoanItems
                .Include(li => li.Product)
                .Where(li => li.LoanId == loanId)
                .ToListAsync();
        }

        public async Task<LoanItem?> GetLoanItemWithProductAsync(int id)
        {
            return await _context.LoanItems
                .Include(li => li.Loan)
            .ThenInclude(l => l.Customer)
        .Include(li => li.Product)
        .FirstOrDefaultAsync(li => li.Id == id);
        }
    }
}
