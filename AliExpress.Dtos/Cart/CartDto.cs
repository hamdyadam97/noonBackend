using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Cart
{
    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal TotalAmount { get; set; }
        public string UserId { get; set; }

    }
}
