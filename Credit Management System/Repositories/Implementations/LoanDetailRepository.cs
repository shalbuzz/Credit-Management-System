using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class LoanDetailRepository : ILoanDetailRepository
    {
        private readonly AppDbContext _context;

        public LoanDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LoanDetail loanDetail)
        {
            await _context.LoanDetails.AddAsync(loanDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int loanId)
        {
            var loanDetail = await GetByLoanIdAsync(loanId);
            if (loanDetail != null)
            {
                _context.LoanDetails.Remove(loanDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LoanDetail>> GetAllWithLoansAsync()
        {
           return await _context.LoanDetails.Include(c => c.Loan)
                .ThenInclude(l => l.Customer)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<LoanDetail?> GetByLoanIdAsync(int loanId)
        {
           return await _context.LoanDetails
                .Include(c => c.Loan)
                .ThenInclude(l => l.Customer)
                .FirstOrDefaultAsync(c => c.LoanId == loanId);
        }

        public async Task UpdateAsync(LoanDetail loanDetail)
        {
            _context.LoanDetails.Update(loanDetail);
            await _context.SaveChangesAsync();
        }
    }
}
