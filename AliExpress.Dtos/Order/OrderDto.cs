using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Order
{
    public class OrderDto
    {
       public int CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AppUserDto ShippingAddress { get; set; }
    }
}
