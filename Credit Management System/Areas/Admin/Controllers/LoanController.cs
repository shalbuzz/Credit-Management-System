﻿using Credit_Management_System.Data;
using Credit_Management_System.Enums;
using Credit_Management_System.Services.Interfaces;
using Credit_Management_System.ViewModels.Loan;
using Credit_Management_System.ViewModels.LoanVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Credit_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Employee")]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;

        public LoanController(ILoanService loanService, IEmployeeService employee, ICustomerService customerService)
        {
            _loanService = loanService;
            _employeeService = employee;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loans = await _loanService.GetLoansWithCustomerAndEmployeeAsync();
            return View(loans);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var loan = await _loanService.GetLoanDetailsAsync(id);
            if (loan == null)
            {
                TempData["Error"] = "Loan not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(loan);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees = await _employeeService.GetAllAsync();
            var customers = await _customerService.GetAllAsync();

            var model = new LoanCreateVM
            {
                Employees = employees.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                }).ToList(),
                Customers = customers.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                }).ToList(),

                // Заполняем список статусов для dropdown
                StatusList = Enum.GetValues(typeof(LoanStatus))
                    .Cast<LoanStatus>()
                    .Select(s => new SelectListItem
                    {
                        Text = s.ToString(),
                        Value = ((int)s).ToString(),
                        Selected = s == LoanStatus.Pending // по умолчанию Pending
                    })
                    .ToList(),

                // По умолчанию статус
                StatusForLoan = LoanStatus.Pending
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanCreateVM loan)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeService.GetAllAsync();
                var customers = await _customerService.GetAllAsync();

                loan.Employees = employees.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                }).ToList();

                loan.Customers = customers.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                }).ToList();

                loan.StatusList = Enum.GetValues(typeof(LoanStatus))
                    .Cast<LoanStatus>()
                    .Select(s => new SelectListItem
                    {
                        Text = s.ToString(),
                        Value = ((int)s).ToString(),
                        Selected = s == loan.StatusForLoan
                    })
                    .ToList();

                return View(loan);
            }

            // Если в модели нет статуса — выставляем по умолчанию Pending
            if (!Enum.IsDefined(typeof(LoanStatus), loan.StatusForLoan))
            {
                loan.StatusForLoan = LoanStatus.Pending;
            }

            bool canTakeLoan = await _loanService.CanCustomerTakeLoanAsync(loan.CustomerId, loan.Amount);

            if (!canTakeLoan)
            {
                ModelState.AddModelError("", "\"The customer has exceeded the credit limit.\".");
                return View(loan);
            }

            await _loanService.CreateVMAsync(loan);
            TempData["Success"] = "Loan created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var loan = await _loanService.GetLoanDetailsVMByCustomerIdAsync(id);
            if (loan == null)
            {
                TempData["Error"] = "Loan not found.";
                return RedirectToAction(nameof(Index));
            }

            var employees = await _employeeService.GetAllAsync();
            var customers = await _customerService.GetAllAsync();

            loan.Employees = employees.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.FullName
            }).ToList();

            loan.Customers = customers.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.FullName
            }).ToList();

            loan.StatusList = Enum.GetValues(typeof(LoanStatus))
                .Cast<LoanStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = ((int)s).ToString(),
                    Selected = s == loan.StatusForLoan
                })
                .ToList();

            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanUpdateVM loan)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeService.GetAllAsync();
                var customers = await _customerService.GetAllAsync();

                loan.Employees = employees.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                }).ToList();

                loan.Customers = customers.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                }).ToList();

                loan.StatusList = Enum.GetValues(typeof(LoanStatus))
                    .Cast<LoanStatus>()
                    .Select(s => new SelectListItem
                    {
                        Text = s.ToString(),
                        Value = ((int)s).ToString(),
                        Selected = s == loan.StatusForLoan
                    })
                    .ToList();

                return View(loan);
            }

            var userRole = User.IsInRole("Admin") ? "Admin" : "Employee";


            await _loanService.UpdateVMAsync(loan, userRole);

            TempData["Success"] = "Loan updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
            {
                TempData["Error"] = "Loan not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(loan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _loanService.DeleteAsync(id);
            TempData["Success"] = "Loan deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
