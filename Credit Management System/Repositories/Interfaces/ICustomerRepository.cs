using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface ICustomerRepository :IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllWithLoansAsync(); 
        Task<Customer?> GetByIdWithLoansAsync(int id); 
    }
}
