using AutoMapper;
using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Branch;
using Credit_Management_System.ViewModels.Category;
using Credit_Management_System.ViewModels.Customer;
using Credit_Management_System.ViewModels.CustomerVM;
using Credit_Management_System.ViewModels.Employee;
using Credit_Management_System.ViewModels.Loan;
using Credit_Management_System.ViewModels.LoanDetail;
using Credit_Management_System.ViewModels.LoanDetailVM;
using Credit_Management_System.ViewModels.LoanItem;
using Credit_Management_System.ViewModels.LoanVM;
using Credit_Management_System.ViewModels.Merchant;
using Credit_Management_System.ViewModels.Payment;
using Credit_Management_System.ViewModels.PaymentVM;
using Credit_Management_System.ViewModels.Product;

namespace Credit_Management_System.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<BranchVM, Branch>().ReverseMap();
            CreateMap<BranchDetailsVM, Branch>().ReverseMap();
            CreateMap<BranchCreateVM, Branch>().ReverseMap();
            CreateMap<BranchUpdateVM, Branch>().ReverseMap();

            CreateMap<EmployeeVM, Employee>().ReverseMap();
            CreateMap<EmployeeCreateVM, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateVM, Employee>().ReverseMap();
            CreateMap<EmployeeDetailsVM, Employee>().ReverseMap();

            CreateMap<MerchantVM, Merchant>().ReverseMap();
            CreateMap<MerchantCreateVM, Merchant>().ReverseMap();
            CreateMap<MerchantUpdateVM, Merchant>().ReverseMap();
            CreateMap<MerchantDetailsVM, Merchant>().ReverseMap();

            CreateMap<CategoryVM, Category>().ReverseMap();
            CreateMap<CategoryCreateVM, Category>().ReverseMap();
            CreateMap<CategoryUpdateVM, Category>().ReverseMap();
            CreateMap<CategoryDetailsVM, Category>().ReverseMap();

            CreateMap<ProductVM, Product>().ReverseMap();
            CreateMap<ProductCreateVM, Product>().ReverseMap();
            CreateMap<ProductUpdateVM, Product>().ReverseMap();
            CreateMap<ProductDetailsVM, Product>().ReverseMap();

            CreateMap<PaymentVM, Payment>().ReverseMap();
            CreateMap<PaymentCreateVM, Payment>().ReverseMap();
            CreateMap<PaymentUpdateVM, Payment>().ReverseMap();
            CreateMap<PaymentDetailsVM, Payment>().ReverseMap();

            CreateMap<CustomerVM, Customer>().ReverseMap();
            CreateMap<CustomerCreateVM, Customer>().ReverseMap();
            CreateMap<CustomerUpdateVM, Customer>().ReverseMap();
            CreateMap<CustomerDetailsVM, Customer>().ReverseMap();

            CreateMap<LoanVM, Loan>().ReverseMap();
            CreateMap<LoanCreateVM, Loan>().ReverseMap();
            CreateMap<LoanUpdateVM, Loan>().ReverseMap();
            CreateMap<LoanDetailsVM, Loan>().ReverseMap();

            CreateMap<LoanDetailVM, LoanDetail>().ReverseMap();
            CreateMap<LoanDetailCreateVM, LoanDetail>().ReverseMap();
            CreateMap<LoanDetailUpdateVM, LoanDetail>().ReverseMap();
            CreateMap<LoanDetailDetailsVM, LoanDetail>().ReverseMap();

            CreateMap<LoanItem, LoanItem>().ReverseMap();
            CreateMap<LoanItemCreateVM, LoanItem>().ReverseMap();
            CreateMap<LoanItemUpdateVM, LoanItem>().ReverseMap();
            CreateMap<LoanItemDetailsVM, LoanItem>().ReverseMap();


        }
    }
    
}
