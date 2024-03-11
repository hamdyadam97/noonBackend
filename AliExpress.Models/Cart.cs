using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Cart
    {

        public int CartId { get; set; }
        public int UserId { get; set; }
        // Other cart properties

        // Navigation properties
        public User User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
