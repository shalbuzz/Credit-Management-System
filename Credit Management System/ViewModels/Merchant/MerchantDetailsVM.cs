﻿using Credit_Management_System.ViewModels.Branch;

namespace Credit_Management_System.ViewModels.Merchant
{
    public class MerchantDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public List<BranchVM> Branches { get; set; } = new();
    }
}
