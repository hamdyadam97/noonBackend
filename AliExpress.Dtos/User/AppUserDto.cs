using AliExpress.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.User
{
    public class AppUserDto
    {
     public string UserId { get; set; }
        public CartDto Cart { get; set; }
    }
}
