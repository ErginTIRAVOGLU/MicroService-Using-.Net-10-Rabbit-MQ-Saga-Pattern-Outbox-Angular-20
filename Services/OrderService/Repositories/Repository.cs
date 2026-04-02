using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Entities;

namespace OrderService.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly OrderContext _dbContext;
    public Repository(OrderContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id : {id} was not found.");

        return entity!;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

}
