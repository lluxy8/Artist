using Core.Abstract;
using Core.Common.Enums;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, bool includeRelated = false);
        Task<List<T>> GetAllAsync(bool includeRelated = false);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        public Task<T?> FindByAsync(Expression<Func<T, bool>> predicate);
        public Task<List<T>> FindAllByAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> TakeAsync(int amount);
        Task<List<T>> TakeAsync(int amount, Expression<Func<T, object>> orderByDescending);
        Task<int> GetCountAsync();
        Task<List<T>> GetByDateFilterAsync(DateTime? startDate = null, DateTime? endDate = null,
            DateFilter predefinedRange = DateFilter.None);

        IQueryable<T> GetQueryable();
    }
}