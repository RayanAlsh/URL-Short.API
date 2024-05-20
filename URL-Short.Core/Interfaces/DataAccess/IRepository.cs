using System.Linq.Expressions;

namespace URL_Short.Core;

public interface IRepository<TEntity> where TEntity : class
{

    Task<TEntity> GetByIdAsync(Guid id);
    Task<IQueryable<TEntity>> GetAllAsync();

    Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();



}