using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Implementations;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Branch;

namespace Credit_Management_System.Services.Implementations
{
    public class BranchService : GenericService<BranchVM, Branch>, IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMerchantRepository _merchantRepository;

        public BranchService(IBranchRepository branchRepository, IMerchantRepository merchantRepository, IMapper mapper)
       : base(branchRepository, mapper)
        {
            _branchRepository = branchRepository;
            _merchantRepository = merchantRepository;
        }

        public async Task<IEnumerable<BranchVM>> GetBranchesWithMerchantAsync()
        {
            var branches = await _branchRepository.GetBranchesWithMerchantAsync();
            return _mapper.Map<IEnumerable<BranchVM>>(branches);
        }

        public async Task<BranchVM?> GetBranchWithEmployeesAsync(int id)
        {
            if (id <= 0) return null;

            var branch = await _branchRepository.GetBranchWithEmployeesAsync(id);
            return branch != null ? _mapper.Map<BranchVM>(branch) : null;
        }

        public async Task<IEnumerable<BranchVM>> GetBranchesByMerchantIdAsync(int merchantId)
        {
            if (merchantId <= 0) return new List<BranchVM>();

            var branches = await _branchRepository.GetBranchesByMerchantIdAsync(merchantId);
            return _mapper.Map<IEnumerable<BranchVM>>(branches);
        }


        public async Task<BranchCreateVM> CreateBranchAsync(BranchCreateVM branchCreateVM)
        {
            return branchCreateVM == null ? null : _mapper.Map<BranchCreateVM>(await _branchRepository.AddAsync(_mapper.Map<Branch>(branchCreateVM)));
        }

        public async Task<BranchUpdateVM> UpdateBranchAsync(BranchUpdateVM branchUpdateVM)
        {
            return branchUpdateVM == null ? null : _mapper.Map<BranchUpdateVM>(await _branchRepository.UpdateAsync(_mapper.Map<Branch>(branchUpdateVM)));
        }

        public async Task<BranchDetailsVM> CreateBranchDetailsAsync(int id)
        {
           var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return null;
            }
            var branchDetails = _mapper.Map<BranchDetailsVM>(branch);
            return branchDetails;
        }

        public async Task<BranchUpdateVM?> GetByIdVMWithMerchantAsync(int id)
        {
            var data = await _branchRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var branchUpdateVM = _mapper.Map<BranchUpdateVM>(data);
            return branchUpdateVM;
        }
    }

}
