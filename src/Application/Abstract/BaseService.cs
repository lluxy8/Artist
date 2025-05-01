using Core.Abstract;
using Core.Common.Enums;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using System.Linq.Expressions;

namespace Application.Abstract
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;

        protected BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(id);

                if (existingEntity != null)
                {
                    await _repository.DeleteAsync(existingEntity);
                }
                else
                {
                    throw new Exception("Entity not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<List<T>> GetAllAsync(bool include = false)
        {
            try
            {
                return await _repository.GetAllAsync(include) ?? [];
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }
        }

        public async Task<T?> GetByIdAsync(Guid id, bool include = false)
        {
            try
            {
                return await _repository.GetByIdAsync(id, include);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity by ID.", ex);
            }
        }

        public virtual Task UpdateAsync(T entity)
        {
            try
            {
                var ExistingEntity = _repository.GetByIdAsync(entity.Id);

                if (ExistingEntity != null)
                {
                    return _repository.UpdateAsync(entity);
                }
                else
                {
                    throw new Exception("Entity not found.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }

        public async Task<List<T>> TakeAsync(int amount)
        {
            try
            {
                return await _repository.TakeAsync(amount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while taking the entities.", ex);
            }
        }

        public async Task<int> GetCountAsync()
        {
            try
            {
                return await _repository.GetCountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the count of entities.", ex);
            }
        }

        public Task<List<T>> GetByDateFilterAsync(DateTime? startDate = null, DateTime? endDate = null
            , DateFilter predefinedRange = DateFilter.None)
        {
            try
            {
                return _repository.GetByDateFilterAsync(startDate, endDate, predefinedRange);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while filtering the entities by date.", ex);
            }
        }

        public Task<List<T>> TakeAsync(int amount, Expression<Func<T, object>> orderByDescending)
        {
            try
            {
                return _repository.TakeAsync(amount, orderByDescending);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while taking the entities with ordering.", ex);
            }
        }
    }
}
