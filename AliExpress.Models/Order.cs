using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Order:BaseEntity
    {
        //Id
        //CreatedAt
        //UpdatedAt



        public decimal TotalAmount { get; set; }
        public string Order_Status { get; set; } = "Processing";

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<OrderItems> Items { get; set; }

       






    }
}
