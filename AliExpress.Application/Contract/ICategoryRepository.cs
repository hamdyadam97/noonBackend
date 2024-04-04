using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface ICategoryRepository:IRepository<Category,int>
    {
        Task<IEnumerable<Product>> getProductByCategoryName(int cateId);
    }
}
