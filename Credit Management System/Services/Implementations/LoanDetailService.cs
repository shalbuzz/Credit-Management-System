using System.Runtime.InteropServices;
using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.LoanDetail;
using Credit_Management_System.ViewModels.LoanDetailVM;

namespace Credit_Management_System.Services.Implementations
{
    public class LoanDetailService : GenericService<LoanDetailVM, LoanDetail>, ILoanDetailService
    {
        private readonly ILoanDetailRepository _loanDetailRepository;

        public LoanDetailService(ILoanDetailRepository loanDetailRepository, IMapper mapper)
            : base(loanDetailRepository, mapper)
        {
            _loanDetailRepository = loanDetailRepository;
        }

        public async Task<LoanDetailVM?> GetByLoanIdAsync(int loanId)
        {
            if (loanId <= 0)
                return null;

            var loanDetail = await _loanDetailRepository.GetByLoanIdAsync(loanId);
            return loanDetail != null ? _mapper.Map<LoanDetailVM>(loanDetail) : null;
        }

        public async Task<LoanDetailUpdateVM> UpdateWithLoansAsync(LoanDetailUpdateVM loanDetailUpdateVM)
        {
           return loanDetailUpdateVM == null ? null : _mapper.Map<LoanDetailUpdateVM>(await _loanDetailRepository.UpdateAsync(_mapper.Map<LoanDetail>(loanDetailUpdateVM)));
        }

        public async Task<LoanDetailCreateVM> CreateWithLoansAsync(LoanDetailCreateVM loanDetailCreateVM)
        {
            return loanDetailCreateVM == null ? null : _mapper.Map<LoanDetailCreateVM>(await _loanDetailRepository.AddAsync(_mapper.Map<LoanDetail>(loanDetailCreateVM)));
        }

        public async Task<IEnumerable<LoanDetailVM>> GetAllWithLoansAsync()
        {
            var loanDetails = await _loanDetailRepository.GetAllWithLoansAsync();
            return _mapper.Map<IEnumerable<LoanDetailVM>>(loanDetails);
        }

        public async Task<LoanDetailVM?> GetByIdWithLoansAsync(int id)
        {
            var data = _loanDetailRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<LoanDetailVM>(data);

        }

        public async Task<LoanDetailDetailsVM?> GetByIdWithLoansAndPaymentsAsync(int id)
        {
            var data = await _loanDetailRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<LoanDetailDetailsVM>(data);
        }

        public async Task<LoanDetailUpdateVM?> GetByIdVMWithLoansAndPaymentsAsync(int id)
        {
            var data = await _loanDetailRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<LoanDetailUpdateVM>(data);
        }
    }


}
