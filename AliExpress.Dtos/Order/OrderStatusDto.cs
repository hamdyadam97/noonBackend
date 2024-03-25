using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Order
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int DeliveryMethodId { get; set; }
        public string Status { get; set; }
    }
}
