﻿using Credit_Management_System.Enums;
using Credit_Management_System.Models;

namespace Credit_Management_System.ViewModels.LoanVM
{
    public class LoanVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }

        public LoanStatus StatusForLoan { get; set; }
    }
    

}
