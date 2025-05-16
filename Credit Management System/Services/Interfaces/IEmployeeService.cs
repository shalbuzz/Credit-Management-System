using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Employee;

namespace Credit_Management_System.Services.Interfaces
{
    public interface IEmployeeService : IGenericService<EmployeeVM,Employee>
    {
        Task<IEnumerable<EmployeeVM>> GetAllWithBranchAndLoansAsync();
        Task<IEnumerable<EmployeeVM>> GetAllWithBranchAsync();
        Task<EmployeeVM?> GetByIdWithBranchAsync(int id);
        Task<IEnumerable<EmployeeVM>> GetEmployeesWithLoansAsync();
        Task<EmployeeVM?> GetEmployeeWithLoansAsync(int id);

        Task<EmployeeCreateVM> CreateEmployeeAsync(EmployeeCreateVM employeeCreateVM);
        Task<EmployeeUpdateVM> UpdateEmployeeAsync(EmployeeUpdateVM employeeUpdateVM);
        Task<EmployeeDetailsVM> CreateEmployeeDetailsAsync(int id);
        Task<EmployeeUpdateVM?> GetByIdVMWithBranchAsync(int id);
    }
}
