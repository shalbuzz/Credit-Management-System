﻿namespace Credit_Management_System.Models
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Loan> Loans { get; set; }

    }
}
