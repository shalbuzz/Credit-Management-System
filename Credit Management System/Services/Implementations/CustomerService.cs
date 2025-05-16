using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Customer;
using Credit_Management_System.ViewModels.CustomerVM;

namespace Credit_Management_System.Services.Implementations
{
    public class CustomerService : GenericService<CustomerVM, Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
            : base(customerRepository, mapper)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<CustomerVM>> GetAllWithLoansAsync()
        {
            var customers = await _customerRepository.GetAllWithLoansAsync();
            return _mapper.Map<IEnumerable<CustomerVM>>(customers);
        }

        public async Task<CustomerVM?> GetByIdWithLoansAsync(int id)
        {
            if (id <= 0)
                return null;

            var customer = await _customerRepository.GetByIdWithLoansAsync(id);
            return customer != null ? _mapper.Map<CustomerVM>(customer) : null;
        }

        public async Task<CustomerUpdateVM> UpdateWithLoansAsync(CustomerUpdateVM customerUpdateVM)
        {
           return customerUpdateVM == null ? null : _mapper.Map<CustomerUpdateVM>(await _customerRepository.UpdateAsync(_mapper.Map<Customer>(customerUpdateVM)));
        }

        public async Task<CustomerCreateVM> CreateWithLoansAsync(CustomerCreateVM customerCreateVM)
        {
           return customerCreateVM == null ? null : _mapper.Map<CustomerCreateVM>(await _customerRepository.AddAsync(_mapper.Map<Customer>(customerCreateVM)));
        }

        public async Task<CustomerVM> DetailWithLoansAsync(int id)
        {
           var data = await _customerRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;

            }
            var customerDetailsVM = _mapper.Map<CustomerVM>(data);
            return customerDetailsVM;

        }
        public async Task<CustomerUpdateVM?> GetByIdVMUpdateVMWithLoansAsync(int id)
        {
            var data = await _customerRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<CustomerUpdateVM>(data);
        }
    }

}
