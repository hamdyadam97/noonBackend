﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class AppUser:IdentityUser
    {
        public Cart Cart { get; set; }

        public string? Code { get; set; }  
        
        // Ayed 
       // public string? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
