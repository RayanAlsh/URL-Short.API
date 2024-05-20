using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using URL_Short.Core;

namespace URL_Short.Infrastructure;

public class URLsRepository : IRepository<URL>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<URL> _dbSet;
    private readonly ILogger<URLsRepository> _logger;
    private string jwtToken;
    public URLsRepository(ApplicationDbContext context, ILogger<URLsRepository> logger)
    {
        _dbContext = context;
        _dbSet = context.Set<URL>();
        _logger = logger;
    }
    public async Task<URL> AddAsync(URL entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while adding a new url");
            return null;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the url");
        }
    }

    public async Task<IQueryable<URL>> GetAllAsync()
    {
        try
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting all urls");
            return Enumerable.Empty<URL>().AsQueryable();
        }
    }

    public async Task<IQueryable<URL>> GetAsync(Expression<Func<URL, bool>> predicate)
    {
        try
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting urls with a predicate");
            return Enumerable.Empty<URL>().AsQueryable();
        }
    }

    public async Task<URL> GetByIdAsync(Guid id)
    {
        try
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting a url by ID");
            return null;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while saving changes to the database");
            throw;
        }
    }

    public async Task<URL> UpdateAsync(URL entity)
    {
        try
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the url");
            return null;
        }
    }
}
