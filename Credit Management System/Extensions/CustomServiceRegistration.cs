using Credit_Management_System.Services.Implementations;
using Credit_Management_System.Services.Interfaces;

namespace Credit_Management_System.Extensions
{
    public static class CustomServiceRegistration
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ILoanItemService, LoanItemService>();
            services.AddScoped<ILoanDetailService, LoanDetailService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
