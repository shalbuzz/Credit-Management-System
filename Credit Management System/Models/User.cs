﻿using Microsoft.AspNetCore.Identity;

namespace Credit_Management_System.Models
{
    public class User : IdentityUser
    {
      public string? LastLoginIp { get; set; }
    }
}
