using Catalog.Domain.Common;

namespace Catalog.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<Tuple<List<T>, long>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(Guid? id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}