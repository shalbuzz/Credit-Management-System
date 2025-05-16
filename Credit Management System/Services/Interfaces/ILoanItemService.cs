using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.LoanItem;
using Credit_Management_System.ViewModels.LoanItemVM;

namespace Credit_Management_System.Services.Interfaces
{
    public interface ILoanItemService : IGenericService<LoanItemVM,LoanItem>
    {
        Task<IEnumerable<LoanItemVM>> GetAllWithLoanAndProductAsync();
        Task<IEnumerable<LoanItemVM>> GetLoanItemsWithProductAsync(int loanId);
        Task<LoanItemVM?> GetLoanItemWithProductAsync(int id);

        Task<LoanItemCreateVM> CreateWithLoanAndProductAsync(LoanItemCreateVM loanItemCreateVM);
        Task<LoanItemUpdateVM> UpdateWithLoanAndProductAsync(LoanItemUpdateVM loanItemUpdateVM);
        Task<LoanItemDetailsVM?> GetByIdWithLoanAndProductAsync(int id);
        Task<LoanItemUpdateVM?> GetByIdVMWithLoanAndProductAsync(int id);
    }
   
}
