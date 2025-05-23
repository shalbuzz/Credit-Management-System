using Credit_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansWithCustomerAndEmployeeAsync();
        Task<Loan?> GetLoanWithDetailsAsync(int id); 
        Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId);
        Task<IEnumerable<Loan>> GetLoansByEmployeeIdAsync(int employeeId);
        Task<List<Loan>> GetAvailableLoansForLoanDetailAsync();
        Task<bool> ExistsAsync(int loanId);
        Task<decimal> GetTotalDebtByCustomerIdAsync(int customerId);

    }

}
