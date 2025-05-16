using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ILoanItemRepository: IGenericRepository<LoanItem>
    {
        Task<IEnumerable<LoanItem>> GetAllWithLoanAndProductAsync();
        Task<IEnumerable<LoanItem>> GetLoanItemsWithProductAsync(int loanId);
        Task<LoanItem?> GetLoanItemWithProductAsync(int id);
    }
}
