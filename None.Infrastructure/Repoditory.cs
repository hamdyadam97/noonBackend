using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class Repoditory<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity, IDeletedEntity
    {
        private readonly AliExpressContext _context;

        public Repoditory(AliExpressContext context)
        {
            _context = context;
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //changed - Haidy
        public async Task DeleteAsync(TEntity entity)
        {
            if (entity is IDeletedEntity deletedEntity)
            {
                deletedEntity.IsDeleted = true;
                _context.Update(entity); // Update the entity in the context
            }
            else
            {
                _context.Set<TEntity>().Remove(entity); // This line is not needed for soft delete
            }

            await _context.SaveChangesAsync();
        }

        //changed - Haidy
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Category))
            {
                return (IEnumerable<TEntity>)await _context.Categories
                    .Include(c => c.Subcategories)
                    .ToListAsync();
            }
            else if (typeof(TEntity) == typeof(Subcategory))
            {
                return (IEnumerable<TEntity>)await _context.Subcategories
                    .Include(s => s.ProductCategories).ThenInclude(pc => pc.Product).ToListAsync();
            }
            else if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>)await _context.Products
                    .Include(p => p.ProductCategories)
                    .Include(p => p.Images)
                    .ToListAsync();
            }
            else
            {
                return _context.Set<TEntity>().AsQueryable();
            }
            return await Task.FromResult(_context.Set<TEntity>().AsQueryable());
        }

        //public async Task<IEnumerable<TEntity>> GetAllAsync()
        //{
        //    var query = _context.Set<TEntity>().AsQueryable();

        //    if (typeof(TEntity) == typeof(Category))
        //    {
        //        query = query.Include(c => ((Category)(object)c).Subcategories);
        //    }
        //    else if (typeof(TEntity) == typeof(Subcategory))
        //    {
        //        query = query.Include(s => ((Subcategory)(object)s).ProductCategories.Select(pc => pc.Product));
        //    }
        //    else if (typeof(TEntity) == typeof(Product))
        //    {
        //        query = query.Include(p => ((Product)(object)p).ProductCategories)
        //                     .Include(p => ((Product)(object)p).Images);
        //    }

        //    return await query.ToListAsync();
        //}

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }




        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }
}
