using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ILoanDetailRepository : IGenericRepository<LoanDetail>
    {
        Task<IEnumerable<LoanDetail>> GetAllWithLoansAsync();
        Task<LoanDetail?> GetByLoanIdAsync(int loanId);
    }
   
}
