using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Subcategory:BaseEntity, IDeletedEntity
    {

        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation property
        public Category Category { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
