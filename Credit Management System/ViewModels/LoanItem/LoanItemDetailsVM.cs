﻿namespace Credit_Management_System.ViewModels.LoanItem
{
    public class LoanItemDetailsVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; }
        public string LoanCustomerName { get; set; }
    }
}
