using Credit_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Credit_Management_System.Components
{
    public class TotalLoanDebtViewComponent : ViewComponent
    {
        private readonly ILoanService _loanService;

        public TotalLoanDebtViewComponent(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            decimal totalDebt = await _loanService.GetTotalDebtByCustomerIdAsync(customerId);
            return View(totalDebt);
        }
    }
}
