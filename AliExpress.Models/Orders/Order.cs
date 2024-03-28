using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models.Orders
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }
        public Order(DeliveryMethod deliveryMethod,AppUser appUser,
            ICollection<OrderItem> items , decimal subtotal)
        {
            DeliveryMethod=deliveryMethod;
            AppUser=appUser;
            OrderItems=items;
            Subtotal=subtotal;

        }

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
        //public int TransactionsID { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
