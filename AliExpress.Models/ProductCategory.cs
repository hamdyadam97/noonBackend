using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
