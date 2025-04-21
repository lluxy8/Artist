using Core.Abstract;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstract
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IDbContextFactory<AppDbContext> _contextfactory;

        public BaseRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextfactory = contextFactory;
        }

        public async Task AddAsync(T entity)
        {
            using var context = _contextfactory.CreateDbContext();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            using var context = _contextfactory.CreateDbContext();
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            using var context = _contextfactory.CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        protected virtual IQueryable<T> IncludeRelatedEntities(IQueryable<T> query) => query;

        public virtual async Task<List<T>> GetAllAsync(bool includeRelated = false)
        {
            using var context = _contextfactory.CreateDbContext();
            var query = context.Set<T>().AsQueryable();

            if (includeRelated)
                query = IncludeRelatedEntities(query);

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, bool includeRelated = false)
        {
            using var context = _contextfactory.CreateDbContext();
            var query = context.Set<T>().AsQueryable();

            if (includeRelated)
                query = IncludeRelatedEntities(query);

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<T> GetQueryable()
        {
            using var context = _contextfactory.CreateDbContext();
            return context.Set<T>().AsQueryable();
        }

        public async Task<T?> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            using var context = _contextfactory.CreateDbContext();
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> FindAllByAsync(Expression<Func<T, bool>> predicate)
        {
            using var context = _contextfactory.CreateDbContext();
            return await context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }
            
    }
}
