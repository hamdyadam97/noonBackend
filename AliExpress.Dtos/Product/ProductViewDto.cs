using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Product
{
    public class ProductViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Title_AR { get; set; }
        public string Description_AR { get; set; }
        public decimal Price { get; set; }
        public List<string> Image { get; set; }
    }
}
