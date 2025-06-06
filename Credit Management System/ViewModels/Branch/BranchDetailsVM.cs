﻿using Credit_Management_System.ViewModels.Employee;

namespace Credit_Management_System.ViewModels.Branch
{
    public class BranchDetailsVM
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string MerchantName { get; set; }

        public List<EmployeeVM> EmployeeVMs { get; set; } = new();
    }
}
