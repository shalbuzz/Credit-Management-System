using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.LoanItem;
using Credit_Management_System.ViewModels.LoanItemVM;

namespace Credit_Management_System.Services.Implementations
{
    public class LoanItemService : GenericService<LoanItemVM, LoanItem>, ILoanItemService
    {
        private readonly ILoanItemRepository _loanItemRepository;

        public LoanItemService(ILoanItemRepository loanItemRepository, IMapper mapper)
            : base(loanItemRepository, mapper)
        {
            _loanItemRepository = loanItemRepository;
        }

      
        public async Task<IEnumerable<LoanItemVM>> GetAllWithLoanAndProductAsync()
        {
            var loanItems = await _loanItemRepository.GetAllWithLoanAndProductAsync();
            return _mapper.Map<IEnumerable<LoanItemVM>>(loanItems);
        }


        public async Task<IEnumerable<LoanItemVM>> GetLoanItemsWithProductAsync(int loanId)
        {
            if (loanId <= 0)
                return Enumerable.Empty<LoanItemVM>();

            var loanItems = await _loanItemRepository.GetLoanItemsWithProductAsync(loanId);
            return _mapper.Map<IEnumerable<LoanItemVM>>(loanItems);
        }

        public async Task<LoanItemVM?> GetLoanItemWithProductAsync(int id)
        {
            if (id <= 0)
                return null;

            var loanItem = await _loanItemRepository.GetLoanItemWithProductAsync(id);
            return loanItem != null ? _mapper.Map<LoanItemVM>(loanItem) : null;
        }

        public async Task<LoanItemUpdateVM> UpdateWithLoanAndProductAsync(LoanItemUpdateVM loanItemUpdateVM)
        {
          return loanItemUpdateVM == null ? null : _mapper.Map<LoanItemUpdateVM>(await _loanItemRepository.UpdateAsync(_mapper.Map<LoanItem>(loanItemUpdateVM)));
        }

        public async Task<LoanItemCreateVM> CreateWithLoanAndProductAsync(LoanItemCreateVM loanItemCreateVM)
        {
            return loanItemCreateVM == null ? null : _mapper.Map<LoanItemCreateVM>(await _loanItemRepository.AddAsync(_mapper.Map<LoanItem>(loanItemCreateVM)));
        }

        public async Task<LoanItemDetailsVM?> GetByIdWithLoanAndProductAsync(int id)
        {
            var data = await _loanItemRepository.GetLoanItemWithProductAsync(id); 
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<LoanItemDetailsVM>(data);
        }

        public async Task<LoanItemUpdateVM?> GetByIdVMWithLoanAndProductAsync(int id)
        {
           var data = await _loanItemRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<LoanItemUpdateVM>(data);
        }
    }


}
