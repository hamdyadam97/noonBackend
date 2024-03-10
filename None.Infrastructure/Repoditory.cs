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
    public class Repoditory<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity
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
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
          return await Task.FromResult(_context.Set<TEntity>().AsQueryable());
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
