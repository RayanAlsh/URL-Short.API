using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using URL_Short.Core;

namespace URL_Short.Infrastructure;

public class UsersRepository : IRepository<User>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<User> _dbSet;
    private readonly ILogger<UsersRepository> _logger;
    private string jwtToken;

    public UsersRepository(ApplicationDbContext context, ILogger<UsersRepository> logger)
    {
        _dbContext = context;
        _dbSet = context.Set<User>();
        _logger = logger;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        try
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting a user by ID");
            return null;
        }
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        try
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting a user by email");
            return null;
        }
    }

    public async Task<IQueryable<User>> GetAllAsync()
    {
        try
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting all users");
            return Enumerable.Empty<User>().AsQueryable();
        }
    }

    public async Task<IQueryable<User>> GetAsync(Expression<Func<User, bool>> predicate)
    {
        try
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting users with a predicate");
            return Enumerable.Empty<User>().AsQueryable();
        }
    }

    public async Task<User> AddAsync(User entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while adding a new user");
            return null;
        }
    }

    public async Task<User> UpdateAsync(User entity)
    {
        try
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the user");
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
            _logger.LogError(e, "An error occurred while deleting the user");
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
}