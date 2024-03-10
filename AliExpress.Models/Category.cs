﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
