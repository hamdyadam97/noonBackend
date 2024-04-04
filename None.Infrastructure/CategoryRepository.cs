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
    public class CategoryRepository : Repoditory<Category, int>, ICategoryRepository
    {
        private readonly AliExpressContext _context;

        public CategoryRepository(AliExpressContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> getProductByCategoryName(int cateId)
        {
            var products = await _context.Products.Include(p => p.Images)
                .Where(p => p.ProductCategories.Any(pc => pc.Category.Id == cateId))
                .ToListAsync();

            return products;
        }
    }
}
