using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.Repositories.Interfaces;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Payment;
using Credit_Management_System.ViewModels.PaymentVM;

namespace Credit_Management_System.Services.Implementations
{
    public class PaymentService : GenericService<PaymentVM, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
            : base(paymentRepository, mapper)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentVM>> GetAllWithLoansAsync()
        {
            var payments = await _paymentRepository.GetAllWithLoansAsync();
            return _mapper.Map<IEnumerable<PaymentVM>>(payments);
        }

        public async Task<PaymentVM?> GetByIdWithLoanAsync(int id)
        {
            if (id <= 0)
                return null;

            var payment = await _paymentRepository.GetByIdWithLoanAsync(id);
            return payment != null ? _mapper.Map<PaymentVM>(payment) : null;
        }

        public async Task<IEnumerable<PaymentVM>> GetPaymentsByLoanIdAsync(int loanId)
        {
            if (loanId <= 0)
                return Enumerable.Empty<PaymentVM>();

            var payments = await _paymentRepository.GetPaymentsByLoanIdAsync(loanId);
            return _mapper.Map<IEnumerable<PaymentVM>>(payments);
        }

        public async Task<PaymentUpdateVM> UpdateWithLoanAsync(PaymentUpdateVM paymentUpdateVM)
        {
           return paymentUpdateVM == null ? null : _mapper.Map<PaymentUpdateVM>(await _paymentRepository.UpdateAsync(_mapper.Map<Payment>(paymentUpdateVM)));
        }

        public async Task<PaymentCreateVM> CreateWithLoanAsync(PaymentCreateVM paymentCreateVM)
        {
           return paymentCreateVM == null ? null : _mapper.Map<PaymentCreateVM>(await _paymentRepository.AddAsync(_mapper.Map<Payment>(paymentCreateVM)));
        }

        public async Task<PaymentDetailsVM?> GetByIdVMWithLoanAsync(int id)
        {
           var data = await _paymentRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<PaymentDetailsVM>(data);

        }
        public async Task<PaymentUpdateVM?> GetByIdVMUpdateVMWithLoanAsync(int id)
        {
           var data = await _paymentRepository.GetByIdAsync(id);
            if (data == null)
                return null;
            return _mapper.Map<PaymentUpdateVM>(data);
        }
    }


}
