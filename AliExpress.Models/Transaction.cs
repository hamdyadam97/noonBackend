using AliExpress.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Transaction: BaseEntity, IDeletedEntity
    {
       // public int TransactionID { get; set; }
        
       
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionStatus { get; set; }

        // Navigation properties
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int PaymentMethodID { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
