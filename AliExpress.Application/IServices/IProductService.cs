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
        Task<PaginationResult<CreateUpdateDeleteProductDto>> GetAllProducts(string searchValue , int page,int pageSize);
        Task<CreateUpdateDeleteProductDto> Create(CreateUpdateDeleteProductDto product);
        Task<CreateUpdateDeleteProductDto> Update(CreateUpdateDeleteProductDto product);
        Task<CreateUpdateDeleteProductDto> Delete(CreateUpdateDeleteProductDto product);
        Task<CreateUpdateDeleteProductDto> GetOne(int Id);
    }
}
