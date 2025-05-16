using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface IBranchRepository:IGenericRepository<Branch>
    {
        Task<IEnumerable<Branch>> GetBranchesWithMerchantAsync();
        Task<Branch?> GetBranchWithEmployeesAsync(int id);
        Task<IEnumerable<Branch>> GetBranchesByMerchantIdAsync(int merchantId);
    }
}
