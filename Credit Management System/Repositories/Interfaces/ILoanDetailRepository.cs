using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ILoanDetailRepository 
    {
        Task<IEnumerable<LoanDetail>> GetAllWithLoansAsync();
        Task<LoanDetail?> GetByLoanIdAsync(int loanId);
        Task AddAsync(LoanDetail loanDetail);
        Task UpdateAsync(LoanDetail loanDetail);
        Task DeleteAsync(int loanId);
    }
   
}
