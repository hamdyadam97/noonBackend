using AliExpress.Dtos.Subcategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<SubCategoryDto> Subcategories { get; set; }
        public ICollection<string> Specification {  get; set; }
        public CategoryDto()
        {
            Subcategories = new List<SubCategoryDto>();
            Specification=new List<string>();

        }
    }
}
