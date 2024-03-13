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
            // Check if page is negative, and handle it appropriately if needed
            

            IQueryable<Product> query = _context.Products;

            // Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
            }
            else
            {
                // If searchValue is null or empty, return all products
                query = query.Where(p => true);
            }
            int skip = 0;
            // Pagination
            if (page <= 1)
            {
                // Handle negative page value (e.g., set it to 1)
                 skip = 1 * pageSize;
            }
            else
            {
                skip = (page - 1) * pageSize;
            }
             
            query = query.Skip(skip).Take(2);

            var result = await  query.ToListAsync();
            return result;
        }

    }
}
