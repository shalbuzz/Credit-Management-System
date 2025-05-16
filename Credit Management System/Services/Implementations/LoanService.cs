using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Loan;
using Credit_Management_System.ViewModels.LoanVM;

namespace Credit_Management_System.Services.Implementations
{
    public class LoanService : GenericService<LoanVM, Loan>, ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository, IMapper mapper)
            : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
        }

        public async Task<IEnumerable<LoanVM>> GetLoansWithCustomerAndEmployeeAsync()
        {
            var loans = await _loanRepository.GetLoansWithCustomerAndEmployeeAsync();
            return _mapper.Map<IEnumerable<LoanVM>>(loans);
        }

        public async Task<LoanVM?> GetLoanWithDetailsAsync(int id)
        {
            if (id <= 0)
                return null;

            var loan = await _loanRepository.GetLoanWithDetailsAsync(id);
            return loan != null ? _mapper.Map<LoanVM>(loan) : null;
        }

        public async Task<IEnumerable<LoanVM>> GetLoansByCustomerIdAsync(int customerId)
        {
            if (customerId <= 0)
                return Enumerable.Empty<LoanVM>();

            var loans = await _loanRepository.GetLoansByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<LoanVM>>(loans);
        }

        public async Task<IEnumerable<LoanVM>> GetLoansByEmployeeIdAsync(int employeeId)
        {
            if (employeeId <= 0)
                return Enumerable.Empty<LoanVM>();

            var loans = await _loanRepository.GetLoansByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<LoanVM>>(loans);
        }

        public async Task<LoanCreateVM> CreateVMAsync(LoanCreateVM loanCreateVM)
        {
           return loanCreateVM == null ? null : _mapper.Map<LoanCreateVM>(await _loanRepository.AddAsync(_mapper.Map<Loan>(loanCreateVM)));
        }

        public async Task<LoanUpdateVM> UpdateVMAsync(LoanUpdateVM loanUpdateVM)
        {
           return loanUpdateVM == null ? null : _mapper.Map<LoanUpdateVM>(await _loanRepository.UpdateAsync(_mapper.Map<Loan>(loanUpdateVM)));
        }

        public async Task<LoanDetailsVM> GetLoanDetailsAsync(int id)
        {
            var data = await _loanRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var loanDetailsVM = _mapper.Map<LoanDetailsVM>(data);
            return loanDetailsVM;
        }

        public async Task<LoanUpdateVM?> GetLoanDetailsVMByCustomerIdAsync(int id)
        {
           var data = await _loanRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var loanDetailsVM = _mapper.Map<LoanUpdateVM>(data);
            return loanDetailsVM;
        }
    }


}
