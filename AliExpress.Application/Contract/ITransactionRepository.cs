using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface ITransactionRepository:IRepository<Transaction,int>
    {
        //Task<IEnumerable<Product>> GetAllAsync(string searchValue, int page, int pageSize);
        Task<int> CoutTransaction();
    }
}
