using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleDistributedCache.Application.Interfaces;
using SimpleDistributedCache.Domain.Entities;
using SimpleDistributedCache.Infrastructure.SqlDb.DbContext;

namespace SimpleDistributedCache.Infrastructure.SqlDb.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _entities = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(decimal id)
    {
        return await _entities.SingleOrDefaultAsync(s => s.Id == id);
    }

    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
    }

    public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }
}