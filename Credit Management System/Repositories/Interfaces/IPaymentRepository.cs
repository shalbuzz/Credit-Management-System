using Credit_Management_System.Models;

namespace Credit_Management_System.Repositories.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetAllWithLoansAsync();
        Task<Payment?> GetByIdWithLoanAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByLoanIdAsync(int loanId);
    }
}
