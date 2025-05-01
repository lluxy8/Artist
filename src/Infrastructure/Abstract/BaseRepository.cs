using Core.Abstract;
using Core.Common.Enums;
using Core.Interfaces.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<T>> TakeAsync(int amount)
        {
            using var context = _contextfactory.CreateDbContext();

            return await context.Set<T>()
                .AsNoTracking()
                .Take(amount)
                .ToListAsync();
        }

        public async Task<List<T>> TakeAsync(int amount, Expression<Func<T, object>> orderByDescending)
        {
            using var context = _contextfactory.CreateDbContext();

            return await context.Set<T>()
                .AsNoTracking()
                .OrderByDescending(orderByDescending)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            using var context = _contextfactory.CreateDbContext();

            return await context.Set<T>()
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<List<T>> GetByDateFilterAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateFilter predefinedRange = DateFilter.None)
        {
            using var context = _contextfactory.CreateDbContext();

            var query = context.Set<T>().AsQueryable();

            if (predefinedRange != DateFilter.None)
            {
                var now = DateTime.UtcNow;

                switch (predefinedRange)
                {
                    case DateFilter.Last24Hours:
                        startDate = now.AddHours(-24);
                        endDate = now;
                        break;
                    case DateFilter.LastWeek:
                        startDate = now.AddDays(-7);
                        endDate = now;
                        break;
                    case DateFilter.LastMonth:
                        startDate = now.AddMonths(-1);
                        endDate = now;
                        break;
                }
            }

            if (startDate.HasValue)
                query = query.Where(x => x.CreateDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.CreateDate <= endDate.Value);

            return await query.ToListAsync();
        }

    }
}
