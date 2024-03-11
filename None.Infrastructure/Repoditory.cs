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
                _context.Update(deletedEntity);
            }
            if (entity.IsDeleted)
            {
                entity.IsDeleted = true;
                _context.Update(deletedEntity);
            }
            else
            {
                _context.Set<TEntity>().Remove(entity);
            }

            await _context.SaveChangesAsync();
        }
        //changed - Haidy
        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Category))
            {
                return (IQueryable<TEntity>)await _context.Categories
                    .Include(c => c.Subcategories)
                    .ToListAsync();
            }
            else if (typeof(TEntity) == typeof(Subcategory))
            {
                return (IQueryable<TEntity>)await _context.Subcategories
                    .Include(s => s.ProductCategories.Select(pc => pc.Product))
                    .ToListAsync();
            }
            else if (typeof(TEntity) == typeof(Product))
            {
                return (IQueryable<TEntity>)await _context.Products
                    .Include(p => p.ProductCategories)
                    .Include(p => p.Images)
                    .ToListAsync();
            }
            else
            {
                return _context.Set<TEntity>().AsQueryable();
            }
            //return await Task.FromResult(_context.Set<TEntity>().AsQueryable());
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
           return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

       
    }
}
