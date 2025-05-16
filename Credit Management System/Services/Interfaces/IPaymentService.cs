using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Payment;
using Credit_Management_System.ViewModels.PaymentVM;

namespace Credit_Management_System.Services.Interfaces
{
    public interface IPaymentService : IGenericService<PaymentVM, Payment>
    {
        Task<IEnumerable<PaymentVM>> GetAllWithLoansAsync();
        Task<PaymentVM?> GetByIdWithLoanAsync(int id);
        Task<IEnumerable<PaymentVM>> GetPaymentsByLoanIdAsync(int loanId);

        Task<PaymentCreateVM> CreateWithLoanAsync(PaymentCreateVM paymentCreateVM);
        Task<PaymentUpdateVM> UpdateWithLoanAsync(PaymentUpdateVM paymentUpdateVM);
        Task<PaymentDetailsVM?> GetByIdVMWithLoanAsync(int id);

        Task<PaymentUpdateVM?> GetByIdVMUpdateVMWithLoanAsync(int id);
    }
}
