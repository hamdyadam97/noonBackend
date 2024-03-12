using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace AliExpress.Models
{
    public class Cart
    {

        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public IdentityUser User { get; set; }
        public int quantity { get; set; }

    }
}
