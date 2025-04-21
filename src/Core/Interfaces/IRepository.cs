using Core.Abstract;
using System.Linq.Expressions;

namespace Core.Interfaces
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
        IQueryable<T> GetQueryable();
    }
}