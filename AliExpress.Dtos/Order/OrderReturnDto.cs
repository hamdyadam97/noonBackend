using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Order
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        ICollection<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
