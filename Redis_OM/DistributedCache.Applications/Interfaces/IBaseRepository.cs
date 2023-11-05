using System.Linq.Expressions;
using DistributedCache.Domain.Entities;

namespace DistributedCache.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity 
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    void Add(T entity);
    Task<bool> SaveChangesAsync();
    Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    void Delete(T entity);
}