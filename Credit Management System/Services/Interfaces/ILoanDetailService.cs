using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.LoanDetail;
using Credit_Management_System.ViewModels.LoanDetailVM;

namespace Credit_Management_System.Services.Interfaces
{
    public interface ILoanDetailService 
    {
        Task<IEnumerable<LoanDetailVM>> GetAllWithLoansAsync();
        Task<LoanDetailVM?> GetByLoanIdAsync(int loanId);
        Task<LoanDetailVM?> GetByIdWithLoansAsync(int id);
        Task<LoanDetailCreateVM> CreateWithLoansAsync(LoanDetailCreateVM loanDetailCreateVM);
        Task<LoanDetailUpdateVM> UpdateWithLoansAsync(LoanDetailUpdateVM loanDetailUpdateVM);
        Task<LoanDetailDetailsVM?> GetByIdWithLoansAndPaymentsAsync(int id);
        Task<LoanDetailUpdateVM?> GetByIdVMWithLoansAndPaymentsAsync(int id);
        Task DeleteAsync(int id);
    }
   
}
