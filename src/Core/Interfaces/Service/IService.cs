using Core.Abstract;
using Core.Common.Enums;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces.Service
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, bool include = false);
        Task<List<T>> GetAllAsync(bool include = false);
        Task<List<T>> TakeAsync(int amount);
        Task<List<T>> TakeAsync(int amount, Expression<Func<T, object>> orderByDescending);
        Task<int> GetCountAsync();
        Task<List<T>> GetByDateFilterAsync(DateTime? startDate = null, DateTime? endDate = null,
            DateFilter predefinedRange = DateFilter.None);
        Task AddAsync(T entity);
        Task DeleteAsync(Guid id); 
        Task UpdateAsync(T entity);
    }
}
