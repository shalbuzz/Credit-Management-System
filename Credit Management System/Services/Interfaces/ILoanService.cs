using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Loan;
using Credit_Management_System.ViewModels.LoanVM;

namespace Credit_Management_System.Services.Interfaces
{
    public interface ILoanService : IGenericService<LoanVM, Loan>
    {
        Task<IEnumerable<LoanVM>> GetLoansWithCustomerAndEmployeeAsync();
        Task<LoanVM?> GetLoanWithDetailsAsync(int id);
        Task<IEnumerable<LoanVM>> GetLoansByCustomerIdAsync(int customerId);
        Task<IEnumerable<LoanVM>> GetLoansByEmployeeIdAsync(int employeeId);

        Task<LoanCreateVM> CreateVMAsync(LoanCreateVM loanCreateVM);
        Task<LoanUpdateVM> UpdateVMAsync(LoanUpdateVM loanUpdateVM);
        Task<LoanDetailsVM> GetLoanDetailsAsync(int id);
        Task<LoanUpdateVM?> GetLoanDetailsVMByCustomerIdAsync(int id);


    }
}
