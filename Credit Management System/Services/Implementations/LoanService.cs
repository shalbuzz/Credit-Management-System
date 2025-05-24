using AutoMapper;
using Credit_Management_System.Enums;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Loan;
using Credit_Management_System.ViewModels.LoanVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Services.Implementations
{
    public class LoanService : GenericService<LoanVM, Loan>, ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private const decimal MaxCreditLimit = 10000m; 

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

        public async Task<LoanUpdateVM> UpdateVMAsync(LoanUpdateVM loanUpdateVM, string userRole)
        {
            if (loanUpdateVM == null)
                return null;

            var existingLoan = await _loanRepository.GetByIdAsync(loanUpdateVM.Id);
            if (existingLoan == null)
                return null;

            bool canChangeStatus = false;

            if (userRole == "Admin")
            {
                canChangeStatus = true; 
            }
            else if (userRole == "Employee")
            {
                if (existingLoan.StatusForLoan == LoanStatus.Pending)
                    canChangeStatus = true;
            }

            if (!canChangeStatus && existingLoan.StatusForLoan != loanUpdateVM.StatusForLoan)
            {
                throw new UnauthorizedAccessException("You do not have permission to change the loan status.");
            }

            var loanToUpdate = _mapper.Map<Loan>(loanUpdateVM);

            if (!canChangeStatus)
            {
                loanToUpdate.StatusForLoan = existingLoan.StatusForLoan; 
            }

            var updatedLoan = await _loanRepository.UpdateAsync(loanToUpdate);

            return _mapper.Map<LoanUpdateVM>(updatedLoan);
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

        public async Task<List<SelectListItem>> GetAvailableLoansForLoanDetailAsync()
        {
            var availableLoans = await _loanRepository.GetAvailableLoansForLoanDetailAsync();
            return availableLoans.Select(loan => new SelectListItem
            {
                Value = loan.Id.ToString(),
                Text = loan.Customer.FullName
            }).ToList();
        }

        public async Task<bool> ExistsAsync(int loanId)
        {
            return await _loanRepository.ExistsAsync(loanId);
        }

        public async Task<decimal> GetTotalDebtByCustomerIdAsync(int customerId)
        {
            return await _loanRepository.GetTotalDebtByCustomerIdAsync(customerId);
        }

        public async  Task<bool> CanCustomerTakeLoanAsync(int customerId, decimal newLoanAmount)
        {
            var totalDebt = await _loanRepository.GetTotalDebtByCustomerIdAsync(customerId);

            return (totalDebt + newLoanAmount) <= MaxCreditLimit;
        }
    }


}
