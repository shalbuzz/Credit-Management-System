using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllWithBranchAndLoansAsync();
        Task<IEnumerable<Employee>> GetAllWithBranchAsync(); 
        Task<Employee> GetByIdWithBranchAsync(int id); 
        Task<IEnumerable<Employee>> GetEmployeesWithLoansAsync(); 
        Task<Employee> GetEmployeeWithLoansAsync(int id); 
    }
}
