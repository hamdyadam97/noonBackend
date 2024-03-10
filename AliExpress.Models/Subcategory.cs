using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Subcategory:BaseEntity
    {

        public string Name { get; set; }
        public int CategoryId { get; set; }

        // Navigation property
        public Category Category { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
