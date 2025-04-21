using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, bool include = false);
        Task<List<T>> GetAllAsync(bool include = false);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
