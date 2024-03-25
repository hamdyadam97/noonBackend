using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace None.Infrastructure
{
    public class ProductRepository : Repoditory<Product, int>, IProductRepository
    {
        private readonly AliExpressContext _context;

        public ProductRepository(AliExpressContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CoutProducts()
        {
            int counts = await _context.Products.CountAsync();
            return counts;
        }



 
public async Task<IEnumerable<Product>> GetAllAsync(string searchValue, int page, int pageSize)
        {
            _context.Subcategories
                    .Include(s => s.ProductCategories).ThenInclude(pc => pc.Product);
            //IQueryable<Product> query= _context.Products.Include(p => p.Images);
            IQueryable<Product> query = from product in _context.Products
                                        join image in _context.Images on product.Id equals image.ProductId into productImages
                                        select new Product
                                        {
                                            // Copy product properties
                                            Id = product.Id,
                                            Title = product.Title,
                                            // Include images
                                            Images = productImages.ToList()
                                        };

            //search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
            }
            int skip = (page - 1) * pageSize;
            //pagination
            query = query.Skip(skip).Take(pageSize);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IQueryable<Product>> SearchByName(string name)
        {
            var product =_context.Products.Where(u => u.Title.ToLower().Contains(name));
            return product;
        }

    }
}
