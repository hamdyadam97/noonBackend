using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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




        //public async Task<IEnumerable<Product>> GetAllAsync(string searchValue, string category, int page, int pageSize)
        //{
        //    _context.Subcategories
        //            .Include(s => s.ProductCategories).ThenInclude(pc => pc.Product);
        //    var cat = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
        //    //IQueryable<Product> query= _context.Products.Include(p => p.Images);
        //    IQueryable<Product> query = from product in _context.Products
        //                                join image in _context.Images on product.Id equals image.ProductId into productImages
        //                                select new Product
        //                                {
        //                                    // Copy product properties
        //                                    Id = product.Id,
        //                                    Title = product.Title,
        //                                    Price = product.Price,
        //                                    Description = product.Description,
        //                                    Accessories = product.Accessories,
        //                                    ActivityTracking = product.ActivityTracking,
        //                                    AgeCategoryApp = product.AgeCategoryApp,
        //                                    AppName = product.AppName,

        //                                    // Include images
        //                                    Images = productImages.ToList()
        //                                };

        //    //search
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
        //    }
        //    else if (!string.IsNullOrEmpty(category))
        //    {
        //        query = query.Where(p => p.Category.id ==cat.Id);
        //    }
        //    int skip = (page - 1) * pageSize;
        //    //pagination
        //    query = query.Skip(skip).Take(pageSize);
        //    var result = await query.ToListAsync();
        //    return result;
        //}


        public async Task<IEnumerable<Product>> GetAllAsync(string searchValue, string category, int page, int pageSize, decimal? minPrice, decimal? maxPrice, string brandName)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Images);

            // Search by product title
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
            }

            // Filter by category using a subquery to avoid fetching full category objects
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.CategoryId == _context.Categories.Where(c => c.Name.ToLower() == category.ToLower()).Select(c => c.Id).FirstOrDefault());
            }

            // Filter by minimum price
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            // Filter by maximum price
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Filter by brand
            if (!string.IsNullOrEmpty(brandName))
            {
                query = query.Where(p => p.Brand == brandName);
            }

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Execute the query
            var result = await query.ToListAsync();
            return result;
        }

































        public async Task<List<Product>> SearchByName(string name)
        {
            var product =_context.Products.Where(u => u.Title.ToLower().Contains(name)).ToList();
            return product;
        }

    }
}
