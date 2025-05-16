using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Payment>> GetAllWithLoansAsync()
        {
            return await _context.Payments.Include(c => c.Loan)
                 .ToListAsync();
        }

        public async Task<Payment?> GetByIdWithLoanAsync(int id)
        {
            return await _context.Payments
                 .Include(c => c.Loan)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByLoanIdAsync(int loanId)
        {
           return await _context.Payments
                .Include(c => c.Loan)
                .Where(c => c.LoanId == loanId)
                .ToListAsync();
        }
    }
}
