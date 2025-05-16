using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Merchant;

namespace Credit_Management_System.Services.Implementations
{
    public class MerchantService : GenericService<MerchantVM, Merchant>, IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository, IMapper mapper)
            : base(merchantRepository, mapper)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<MerchantDetailsVM?> GetByIdWithBranchAsync(int id)
        {
            var data = await _merchantRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<MerchantDetailsVM>(data);

        }

        public async Task<MerchantVM?> GetByIdWithBranchesAsync(int id)
        {
            if (id <= 0)
                return null;

            var merchant = await _merchantRepository.GetByIdWithBranchesAsync(id);
            return merchant != null ? _mapper.Map<MerchantVM>(merchant) : null;
        }

        public async Task<MerchantUpdateVM> UpdateWithBranchAsync(MerchantUpdateVM merchantUpdateVM)
        {
           return merchantUpdateVM == null ? null : _mapper.Map<MerchantUpdateVM>(await _merchantRepository.UpdateAsync(_mapper.Map<Merchant>(merchantUpdateVM)));
        }


        public async Task<MerchantCreateVM> CreateWithBranchAsync(MerchantCreateVM merchantCreateVM)
        {
            return merchantCreateVM == null ? null : _mapper.Map<MerchantCreateVM>(await _merchantRepository.AddAsync(_mapper.Map<Merchant>(merchantCreateVM)));
        }

        public async Task<IEnumerable<MerchantVM>> GetAllWithBranchesAsync()
        {
            var merchants = await _merchantRepository.GetAllWithBranchesAsync();
            return _mapper.Map<IEnumerable<MerchantVM>>(merchants);
        }

        public async Task<MerchantUpdateVM?> GetByIdVMUpdateVMWithBranchAsync(int id)
        {
            var data = await _merchantRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<MerchantUpdateVM>(data);
        }
    }


}
