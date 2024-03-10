using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(string searchValue,int page,int pageSize);
        Task<int> CoutProducts();
    }
}
