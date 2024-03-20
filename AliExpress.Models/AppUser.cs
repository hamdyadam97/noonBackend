using AliExpress.Models.Orders;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class AppUser:IdentityUser
    {
        public string City { get; set; }=string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string specialPlace { get; set; } = string.Empty;
        public Cart Cart { get; set; }
        
       
        public ICollection<Order> Orders { get; set; }=new HashSet<Order>();
    }
}
