using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Branch;

namespace Credit_Management_System.Services.Interfaces
{
    public interface IBranchService:IGenericService<BranchVM, Branch>
    {
        Task<IEnumerable<BranchVM>> GetBranchesWithMerchantAsync();
        Task<BranchVM?> GetBranchWithEmployeesAsync(int id);
        Task<IEnumerable<BranchVM>> GetBranchesByMerchantIdAsync(int merchantId);

        Task<BranchCreateVM> CreateBranchAsync(BranchCreateVM branchCreateVM);
        Task<BranchUpdateVM> UpdateBranchAsync(BranchUpdateVM branchUpdateVM);
        Task<BranchDetailsVM> CreateBranchDetailsAsync(int id);
        Task<BranchUpdateVM?> GetByIdVMWithMerchantAsync(int id);
    }
}
