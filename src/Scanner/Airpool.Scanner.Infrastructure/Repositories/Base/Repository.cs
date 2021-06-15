using Airpool.Scanner.Core.Entities.Base;
using Airpool.Scanner.Core.Repository.Base;
using Airpool.Scanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Infrastructure.Repositories.Base
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity
    {
        protected readonly ScannerContext _dbContext;

        public Repository(ScannerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true,
            string includeString = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            if (predicate != null)
                query = query.Where(predicate);
            return await ((orderBy != null) ? orderBy(query) : query).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,  
            bool disableTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicate != null)
                query = query.Where(predicate);
            return await ((orderBy != null) ? orderBy(query) : query).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
