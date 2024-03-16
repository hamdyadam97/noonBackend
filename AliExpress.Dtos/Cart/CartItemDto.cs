using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Cart
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }

       public int CartId { get; set; }
    }
}
