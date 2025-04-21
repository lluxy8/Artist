using Core.Abstract;
using Core.Interfaces;

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

        public async Task DeleteAsync(T entity)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(entity.Id);

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

        public Task UpdateAsync(T entity)
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
    }
}
