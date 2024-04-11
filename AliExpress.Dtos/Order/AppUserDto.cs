using AliExpress.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Order
{
    public class AppUserDto
    {
        public string? Id { get; set; }
        public string? City { get; set; } 
        public string? Country { get; set; } 
        public string? Phone { get; set; } 
        public CartDto? Cart { get; set; }
        public string? specialPlace { get; set; } 
    }
}
