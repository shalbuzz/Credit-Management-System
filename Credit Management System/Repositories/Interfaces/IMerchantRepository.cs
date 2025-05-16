using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface IMerchantRepository : IGenericRepository<Merchant>
    {
        Task<IEnumerable<Merchant>> GetAllWithBranchesAsync();
        Task<Merchant?> GetByIdWithBranchesAsync(int id);
       
    }
}
