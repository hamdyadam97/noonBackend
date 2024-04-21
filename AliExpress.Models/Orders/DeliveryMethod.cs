using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models.Orders
{
    public class DeliveryMethod:BaseEntity
    {
        public string ?Name { get; set; }
        public string ?Description { get; set; }
        public string? Name__Ar { get; set; }
        public string? Description_Ar { get; set; }
        public decimal Cost { get; set; }
        public string ?DeliveryTime { get; set; }
    }
}
