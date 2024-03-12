using AliExpress.Dtos.Category;
using AliExpress.Dtos.Product;
using AliExpress.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<ResultView<CategoryDto>> Create(CategoryDto categoryDto);
        Task<ResultView<CategoryDto>> Update(CategoryDto categoryDto);
        Task Delete(CategoryDto categoryDto);
        Task<ResultView<CategoryDto>> GetOne(int Id);
    }
}
