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
        public decimal TotalAmount { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        //relation-many
        public ICollection<CartItem> CartItems { get; set; }

    }
}
