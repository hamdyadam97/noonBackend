using AliExpress.Dtos.Order;
using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Payment
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionStatus { get; set; }

        // Navigation properties
        public int OrderID { get; set; }
        public OrderDto? Order { get; set; }
        public int PaymentMethodID { get; set; }
        public PaymentMethodDTO? PaymentMethod { get; set; }
    }
}
