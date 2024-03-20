using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models.Orders
{
    public class Order:BaseEntity
    {
       
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int OrderItemId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GetTotal()
           => Subtotal + DeliveryMethod.Cost;
    }
}
