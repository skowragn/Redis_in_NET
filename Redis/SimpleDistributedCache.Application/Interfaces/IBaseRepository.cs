using System.Linq.Expressions;
using SimpleDistributedCache.Domain.Entities;

namespace SimpleDistributedCache.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity 
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(decimal id);
    void Add(T entity);
    Task<bool> SaveChangesAsync();
    Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    void Delete(T entity);
}