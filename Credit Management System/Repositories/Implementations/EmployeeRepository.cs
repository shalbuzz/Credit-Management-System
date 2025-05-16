using Credit_Management_System.Data;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Credit_Management_System.Repositories.Implementations
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllWithBranchAndLoansAsync()
        {
            return await _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.LoansHandled)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllWithBranchAsync()
        {
            return await _context.Employees
                .Include(e => e.Branch)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<Employee> GetByIdWithBranchAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Branch)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithLoansAsync()
        {
            return await _context.Employees
                .Include(e => e.LoansHandled)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeWithLoansAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.LoansHandled)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}