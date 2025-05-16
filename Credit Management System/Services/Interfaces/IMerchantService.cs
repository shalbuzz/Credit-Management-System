using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Merchant;

namespace Credit_Management_System.Services.Interfaces
{
    public interface IMerchantService : IGenericService<MerchantVM, Merchant>
    {
        Task<IEnumerable<MerchantVM>> GetAllWithBranchesAsync();
        Task<MerchantVM?> GetByIdWithBranchesAsync(int id);

        Task<MerchantCreateVM> CreateWithBranchAsync(MerchantCreateVM merchantCreateVM);
        Task<MerchantUpdateVM> UpdateWithBranchAsync(MerchantUpdateVM merchantUpdateVM);
        Task<MerchantDetailsVM?> GetByIdWithBranchAsync(int id);
        Task<MerchantUpdateVM?> GetByIdVMUpdateVMWithBranchAsync(int id);
    }
}
