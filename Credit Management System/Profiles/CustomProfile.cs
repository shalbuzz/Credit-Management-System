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
using Credit_Management_System.ViewModels.LoanItemVM;
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
            CreateMap<Branch, BranchDetailsVM>()
    .ForMember(dest => dest.MerchantName, opt => opt.MapFrom(src => src.Merchant.Name))
    .ForMember(dest => dest.EmployeeVMs, opt => opt.MapFrom(src => src.Employees));



            CreateMap<EmployeeVM, Employee>().ReverseMap();
            CreateMap<EmployeeCreateVM, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateVM, Employee>().ReverseMap();
            CreateMap<EmployeeDetailsVM, Employee>().ReverseMap();
            CreateMap<Employee, EmployeeDetailsVM>()
    .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name));


            CreateMap<MerchantVM, Merchant>().ReverseMap();
            CreateMap<MerchantCreateVM, Merchant>().ReverseMap();
            CreateMap<MerchantUpdateVM, Merchant>().ReverseMap();
            CreateMap<MerchantDetailsVM, Merchant>().ReverseMap();
            CreateMap<Merchant, MerchantDetailsVM>()
     .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches));


            CreateMap<CategoryVM, Category>().ReverseMap();
            CreateMap<CategoryCreateVM, Category>().ReverseMap();
            CreateMap<CategoryUpdateVM, Category>().ReverseMap();
            CreateMap<CategoryDetailsVM, Category>().ReverseMap();
            CreateMap<Category, CategoryDetailsVM>()
    .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : "None"))
    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));


            CreateMap<ProductVM, Product>().ReverseMap();
            CreateMap<Product, ProductVM>()
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<ProductCreateVM, Product>().ReverseMap();
            CreateMap<ProductUpdateVM, Product>().ReverseMap();
            CreateMap<ProductDetailsVM, Product>().ReverseMap();
            CreateMap<Product, ProductDetailsVM>()
   .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
   .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));




            CreateMap<PaymentVM, Payment>().ReverseMap();
            CreateMap<PaymentCreateVM, Payment>().ReverseMap();
            CreateMap<PaymentUpdateVM, Payment>().ReverseMap();
            CreateMap<PaymentDetailsVM, Payment>().ReverseMap();

            CreateMap<CustomerVM, Customer>().ReverseMap();
            CreateMap<CustomerCreateVM, Customer>().ReverseMap();
            CreateMap<CustomerUpdateVM, Customer>().ReverseMap();
            CreateMap<CustomerDetailsVM, Customer>().ReverseMap();

            CreateMap<LoanVM, Loan>().ReverseMap();
            CreateMap<Loan, LoanVM>()
    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id));
            CreateMap<LoanCreateVM, Loan>().ReverseMap();
            CreateMap<LoanUpdateVM, Loan>().ReverseMap();
            CreateMap<LoanDetailsVM, Loan>().ReverseMap();
            CreateMap<Loan, LoanDetailsVM>()
    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));


            CreateMap<LoanDetailVM, LoanDetail>().ReverseMap();
            CreateMap<LoanDetailCreateVM, LoanDetail>().ReverseMap();
            CreateMap<LoanDetailUpdateVM, LoanDetail>().ReverseMap();
            CreateMap<LoanDetailDetailsVM, LoanDetail>().ReverseMap();


            CreateMap<LoanItemVM, LoanItem>().ReverseMap();
            CreateMap<LoanItemCreateVM, LoanItem>().ReverseMap();
            CreateMap<LoanDetailUpdateVM, LoanDetail>()
     .ForMember(dest => dest.LoanId, opt => opt.Ignore());
            CreateMap<LoanItemDetailsVM, LoanItem>().ReverseMap();
            CreateMap<LoanItem, LoanItemDetailsVM>()
     .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
     .ForMember(dest => dest.LoanCustomerName, opt => opt.MapFrom(src => src.Loan.Customer.FullName));



        }
    }
    
}
