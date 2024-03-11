using AliExpress.Dtos.Category;
using AliExpress.Dtos.Subcategory;
using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryDto>> GetAllCategory();
        Task<SubCategoryDto> Create(SubCategoryDto subcategoryDto);
        Task<SubCategoryDto> Update(SubCategoryDto subcategoryDto);
        Task Delete(SubCategoryDto subcategoryDto);
        Task<SubCategoryDto> GetOne(int Id);
    }
}
