using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Numerics;

namespace DataAccess.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransaction();
    Task Commit();
    Task Rollback();

    DbSet<TEntity> Table { get; }

    Task<int> SaveChangesAsync();
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetFirstAsync();
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetFirstAsyncWithInclude(Expression<Func<TEntity, bool>> predicate, string include);
    IQueryable<TEntity> GetQuery(bool isDeleted = false);
    IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    //Task<bool> EntityIsExistAsync(long id);
    Task<int> AddRangeAsync(List<TEntity> entity, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> AddToDbSet(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(List<TEntity> entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
    Task AddRangeToDbSetAsync(List<TEntity> entity);
}
