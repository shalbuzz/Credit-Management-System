﻿using Credit_Management_System.Models;
using Credit_Management_System.ViewModels.Loan;
using Credit_Management_System.ViewModels.LoanVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Credit_Management_System.Services.Interfaces
{
    public interface ILoanService : IGenericService<LoanVM, Loan>
    {
        Task<IEnumerable<LoanVM>> GetLoansWithCustomerAndEmployeeAsync();
        Task<LoanVM?> GetLoanWithDetailsAsync(int id);
        Task<IEnumerable<LoanVM>> GetLoansByCustomerIdAsync(int customerId);
        Task<IEnumerable<LoanVM>> GetLoansByEmployeeIdAsync(int employeeId);

        Task<LoanCreateVM> CreateVMAsync(LoanCreateVM loanCreateVM);
        Task<LoanUpdateVM> UpdateVMAsync(LoanUpdateVM loanUpdateVM, string userRole);
        Task<LoanDetailsVM> GetLoanDetailsAsync(int id);
        Task<LoanUpdateVM?> GetLoanDetailsVMByCustomerIdAsync(int id);
        Task<List<SelectListItem>> GetAvailableLoansForLoanDetailAsync();

        Task<bool> ExistsAsync(int loanId);
        Task<decimal> GetTotalDebtByCustomerIdAsync(int customerId);
        Task<bool> CanCustomerTakeLoanAsync(int customerId, decimal newLoanAmount);



    }
}
