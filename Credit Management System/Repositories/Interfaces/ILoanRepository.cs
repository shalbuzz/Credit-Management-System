using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansWithCustomerAndEmployeeAsync();
        Task<Loan?> GetLoanWithDetailsAsync(int id); 
        Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId);
        Task<IEnumerable<Loan>> GetLoansByEmployeeIdAsync(int employeeId);
    }
   
}
