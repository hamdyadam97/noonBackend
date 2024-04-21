using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class PaymentMethod: BaseEntity, IDeletedEntity
    {
       // public int PaymentMethodID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Name_AR { get; set; }
        public string Description_AR { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation property
        public ICollection<Transaction> Transactions { get; set; }
    }
}
