using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Services
{
    public interface IProductService
    {
        Task<PaginationResult<ProductViewDto>> GetAllProducts(string searchValue , int page,int pageSize);
        Task<CreateUpdateDeleteProductDto> Create(CreateUpdateDeleteProductDto productDto);
        Task<CreateUpdateDeleteProductDto> Update(CreateUpdateDeleteProductDto productDto);
        Task Delete(CreateUpdateDeleteProductDto productDto);
        Task<CreateUpdateDeleteProductDto> GetOne(int Id);
    }
}
