using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class ProductRepository : Repoditory<Product, int>, IProductRepository
    {
        private readonly AliExpressContext _context;

        public ProductRepository(AliExpressContext context):base(context) 
        {
            _context = context;
        }
        public async Task<int> CoutProducts()
        {
           int counts= await  _context.Products.CountAsync();
            return counts;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string searchValue, int page, int pageSize)
        {
            IQueryable<Product> query=_context.Products;
            //search
            if(!string.IsNullOrEmpty(searchValue))
            {
                query=query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
            }
            int skip=(page-1) * pageSize;
            //pagination
            query=query.Skip(skip).Take(pageSize);
            var result= await query.ToListAsync();
            return result;
        }
    }
}
