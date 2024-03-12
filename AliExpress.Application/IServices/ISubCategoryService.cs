using AliExpress.Dtos.Category;
using AliExpress.Dtos.Subcategory;
using AliExpress.Dtos.ViewResult;
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
        Task<ResultList <SubCategoryDto>> GetAllCategory();
        Task<ResultView<SubCategoryDto>> Create(SubCategoryDto subcategoryDto);
        Task<ResultView<SubCategoryDto>> Update(SubCategoryDto subcategoryDto);
        Task<ResultView<SubCategoryDto>> Delete(int id);
        Task<ResultView<SubCategoryDto>> GetOne(int Id);
    }
}
