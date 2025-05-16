using Credit_Management_System.Repositories.Implementations;
using Credit_Management_System.Repositories.Interfaces;

namespace Credit_Management_System.Extensions
{
    public static class CustomRepositoryRegistration
    {
        public static void AddCustomRepository(this IServiceCollection services)
        {
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ILoanRepository,LoanRepostiory>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ILoanItemRepository, LoanItemRepository>();
            services.AddScoped<ILoanDetailRepository, LoanDetailRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
