using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
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
    }
}
