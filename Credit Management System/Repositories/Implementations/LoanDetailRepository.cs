using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class LoanDetailRepository :GenericRepository<LoanDetail>, ILoanDetailRepository
    {
        private readonly AppDbContext _context;
        public LoanDetailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanDetail>> GetAllWithLoansAsync()
        {
           return await _context.LoanDetails.Include(c => c.Loan)
                .ToListAsync();
        }

        public async Task<LoanDetail?> GetByLoanIdAsync(int loanId)
        {
           return await _context.LoanDetails
                .Include(c => c.Loan)
                .FirstOrDefaultAsync(c => c.LoanId == loanId);
        }
    }
}
