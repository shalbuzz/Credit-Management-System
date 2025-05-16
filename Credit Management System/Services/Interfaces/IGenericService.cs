using Credit_Management_System.Models;

namespace Credit_Management_System.Services.Interfaces
{
    public interface IGenericService<TVM, T> where TVM : class where T : BaseEntity, new()
    {
        Task<IEnumerable<TVM>> GetAllAsync();
        Task<TVM?> GetByIdAsync(int id);
        Task<TVM> CreateAsync(TVM entity);
        Task<TVM> UpdateAsync(TVM entity);
        Task<bool> DeleteAsync(int id);
    }
  
}
