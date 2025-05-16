using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class CustomerRepository :GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllWithLoansAsync()
        {
           return await _context.Customers.Include(c => c.Loans).Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdWithLoansAsync(int id)
        {
           return await _context.Customers
                .Include(c => c.Loans)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
