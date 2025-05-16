using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Customer;
using Credit_Management_System.ViewModels.CustomerVM;

namespace Credit_Management_System.Services.Interfaces
{
    public interface ICustomerService :IGenericService<CustomerVM,Customer>
    {
        Task<IEnumerable<CustomerVM>> GetAllWithLoansAsync();
        Task<CustomerVM?> GetByIdWithLoansAsync(int id);

        Task<CustomerCreateVM> CreateWithLoansAsync(CustomerCreateVM customerCreateVM);
        Task<CustomerUpdateVM> UpdateWithLoansAsync(CustomerUpdateVM customerUpdateVM);
        Task<CustomerVM?> DetailWithLoansAsync(int id);
        Task<CustomerUpdateVM?> GetByIdVMUpdateVMWithLoansAsync(int id);
    }
}
