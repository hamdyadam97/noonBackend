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


        public async Task<IEnumerable<Product>> GetAllAsync(string searchValue, string category, int page, int pageSize, decimal minPrice, decimal maxPrice, string brandName)
        {
            IQueryable<Product> query = _context.Products;

            // Include images
            query = query.Include(product => product.Images);

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
            }
            else if (!string.IsNullOrEmpty(category))
            {
                var cat = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
                if (cat != null)
                {
                    query = query.Where(p => p.Category == cat.Id.ToString());
                }
            }

            if(minPrice!=-1) 
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if(maxPrice!=-1) 
            {
                query=query.Where(p => p.Price <= maxPrice);
            }


            if(!string.IsNullOrEmpty(brandName))
            {
                query=query.Where(p=>p.Brand==brandName);
            }

            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

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
