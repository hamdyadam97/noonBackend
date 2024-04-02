using AliExpress.Dtos.Pagination;
using AliExpress.Dtos.Product;
using AliExpress.Dtos.Subcategory;
using AliExpress.Dtos.ViewResult;
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
        Task<ResultView<CreateUpdateDeleteProductDto>> Create(CreateUpdateDeleteProductDto productDto);
        Task<ResultView<CreateUpdateDeleteProductDto>> Update(CreateUpdateDeleteProductDto productDto);
        Task<ResultView<CreateUpdateDeleteProductDto>> Delete(int id);
        Task<ResultView<CreateUpdateDeleteProductDto>> GetOne(int Id);
        Task<int> countProducts();
        //Task<ResultView<ProductViewDto>> Search(string name);
    }
}
