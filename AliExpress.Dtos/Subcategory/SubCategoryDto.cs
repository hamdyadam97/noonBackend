using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Subcategory
{
    public class SubCategoryDto
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int CategoryId { get; set; }

        public string Category { get; set; }

        public virtual ICollection<string> ProductCategories { get; set; }
    }
}
