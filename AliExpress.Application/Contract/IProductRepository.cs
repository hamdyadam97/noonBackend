using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface IProductRepository :IRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetAllAsync(string searchValue, string category, int page, int pageSize, decimal minPrice, decimal maxPrice, string brandName);
        Task<int> CoutProducts();
        Task<List<Product>> SearchByName(string name);
        
    }
}
