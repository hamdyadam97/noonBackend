using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Category: BaseEntity, IDeletedEntity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Subcategory> Subcategories { get; set; }
    
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
