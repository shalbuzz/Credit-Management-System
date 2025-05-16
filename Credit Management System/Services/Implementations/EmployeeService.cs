using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Employee;

namespace Credit_Management_System.Services.Implementations
{
   public class EmployeeService : GenericService<EmployeeVM, Employee>, IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        : base(employeeRepository, mapper)
    {
        _employeeRepository = employeeRepository;
    }

        public async Task<IEnumerable<EmployeeVM>> GetAllWithBranchAndLoansAsync()
    {
        var employees = await _employeeRepository.GetAllWithBranchAndLoansAsync();
        return _mapper.Map<IEnumerable<EmployeeVM>>(employees);
    }

    public async Task<IEnumerable<EmployeeVM>> GetAllWithBranchAsync()
    {
        var employees = await _employeeRepository.GetAllWithBranchAsync();
        return _mapper.Map<IEnumerable<EmployeeVM>>(employees);
    }

    public async Task<EmployeeVM?> GetByIdWithBranchAsync(int id)
    {
        if (id <= 0) return null;

        var employee = await _employeeRepository.GetByIdWithBranchAsync(id);
        return employee != null ? _mapper.Map<EmployeeVM>(employee) : null;
    }

    public async Task<IEnumerable<EmployeeVM>> GetEmployeesWithLoansAsync()
    {
        var employees = await _employeeRepository.GetEmployeesWithLoansAsync();
        return _mapper.Map<IEnumerable<EmployeeVM>>(employees);
    }

    public async Task<EmployeeVM?> GetEmployeeWithLoansAsync(int id)
    {
        if (id <= 0) return null;

        var employee = await _employeeRepository.GetEmployeeWithLoansAsync(id);
        return employee != null ? _mapper.Map<EmployeeVM>(employee) : null;
    }

        public async Task<EmployeeUpdateVM> UpdateEmployeeAsync(EmployeeUpdateVM employeeUpdateVM)
        {
            return employeeUpdateVM == null ? null : _mapper.Map<EmployeeUpdateVM>(await _employeeRepository.UpdateAsync(_mapper.Map<Employee>(employeeUpdateVM)));
        }

        public async Task<EmployeeCreateVM> CreateEmployeeAsync(EmployeeCreateVM employeeCreateVM)
        {
            return employeeCreateVM == null ? null : _mapper.Map<EmployeeCreateVM>(await _employeeRepository.AddAsync(_mapper.Map<Employee>(employeeCreateVM)));
        }

        public async Task<EmployeeDetailsVM> CreateEmployeeDetailsAsync(int id)
        {
            var data = await _employeeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var employeeDetails = _mapper.Map<EmployeeDetailsVM>(data);
            return employeeDetails;
        }

        public async Task<EmployeeUpdateVM?> GetByIdVMWithBranchAsync(int id)
        {
            var data = await _employeeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var employeeUpdateVM = _mapper.Map<EmployeeUpdateVM>(data);
            return employeeUpdateVM;
        }
    }

}
