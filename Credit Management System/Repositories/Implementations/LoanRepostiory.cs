using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class LoanRepostiory : GenericRepository<Loan>, ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepostiory(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int loanId)
        {
            return await _context.Loans.AnyAsync(l => l.Id == loanId && !l.IsDeleted);
        }

        public async Task<List<Loan>> GetAvailableLoansForLoanDetailAsync()
        {
            return await _context.Loans
                .Include(l => l.Customer)
                .Where(l => !l.IsDeleted && !_context.LoanDetails
                    .Any(ld => ld.LoanId == l.Id && !ld.IsDeleted))
                .ToListAsync();
        }



        public async Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId)
        {
            return await _context.Loans
                .Include(l => l.Customer)
                .Include(l => l.Employee)
                .Where(l => l.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByEmployeeIdAsync(int employeeId)
        {
            return await _context.Loans
                .Include(l => l.Customer)
                .Include(l => l.Employee)
                .Where(l => l.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansWithCustomerAndEmployeeAsync()
        {
            return await _context.Loans
                 .Include(l => l.Customer)
                 .Include(l => l.Employee)
                 .Where(l => !l.IsDeleted)
                 .ToListAsync();
        }

        public async Task<Loan?> GetLoanWithDetailsAsync(int id)
        {
            return await _context.Loans
                .Include(l => l.Customer)
                .Include(l => l.Employee)
                .Include(l => l.LoanDetail)
                .Include(l => l.LoanItems)
                    .ThenInclude(li => li.Product)
                .Include(l => l.Payments)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<decimal> GetTotalDebtByCustomerIdAsync(int customerId)
        {
            var loans = await _context.Loans
            .Include(l => l.LoanItems)
            .Include(l => l.Payments)
            .Where(l => l.CustomerId == customerId)
            .ToListAsync();

            decimal totalLoan = loans.SelectMany(l => l.LoanItems).Sum(i => i.TotalAmount);
            decimal totalPaid = loans.SelectMany(l => l.Payments).Sum(p => p.Amount);
            decimal debt = totalLoan - totalPaid;

            return debt < 0 ? 0 : debt;
        }
    }
}
