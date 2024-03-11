using AliExpress.Dtos.Category;
using AliExpress.Dtos.Product;
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
        Task<CategoryDto> Create(CategoryDto categoryDto);
        Task<CategoryDto> Update(CategoryDto categoryDto);
        Task Delete(CategoryDto categoryDto);
        Task<CategoryDto> GetOne(int Id);
    }
}
