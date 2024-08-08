using Core.Entity;
using Core.Exceptions;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase , IDeletedEntity
{
    protected readonly BookManagemenContext Context;
    protected readonly DbSet<TEntity> DbSet;
    private DatabaseFacade Database => Context.Database;
    private IDbContextTransaction? Transaction => Database.CurrentTransaction;

    public DbSet<TEntity> Table => DbSet;

    public BaseRepository(BookManagemenContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = context.Set<TEntity>();
    }

    public async Task BeginTransaction()
    {
        if (Transaction is null) await Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        if (Transaction is not null) await Database.CommitTransactionAsync();
    }

    public async Task Rollback()
    {
        if (Transaction is not null) await Database.RollbackTransactionAsync();
    }

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();


    public IQueryable<TEntity> GetQuery(bool isDeleted = false)
    {
        return DbSet.Where(x => x.Deleted == isDeleted).AsQueryable();
    }

    public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.Where(x => !x.Deleted).Where(predicate).AsQueryable();
    }

    public IQueryable<TEntity> GetQueryWithInclude(string include)
    {
        return DbSet.Where(x => !x.Deleted).Include(include).AsQueryable();
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return DbSet.Where(x => !x.Deleted).ToListAsync();
    }

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.Where(x => !x.Deleted).Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetFirstAsync()
    {
        var entity = await DbSet.Where(x => !x.Deleted).FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }
    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await DbSet.Where(x => !x.Deleted).Where(predicate).FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.Where(x => !x.Deleted).Where(predicate).FirstOrDefaultAsync();
    }

    public Task<TEntity> GetFirstAsyncWithInclude(Expression<Func<TEntity, bool>> predicate, string include)
    {
        var entity = DbSet.Where(x => !x.Deleted).Where(predicate).Include(include).FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    //public Task<bool> EntityIsExistAsync(long id)
    //{
    //    return DbSet.AnyAsync(x => x.Id == id);
    //}

    public async Task<int> AddRangeAsync(List<TEntity> entity, CancellationToken cancellationToken = default)
    {
        await AddRangeToDbSetAsync(entity);
        var count = await Context.SaveChangesAsync(cancellationToken);
        return count;
    }

    public async Task AddRangeToDbSetAsync(List<TEntity> entity)
    {
        await DbSet.AddRangeAsync(entity);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = await AddToDbSet(entity);
        await Context.SaveChangesAsync();
        return addedEntity;
    }

    public async Task<TEntity> AddToDbSet(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.Deleted = true;
        var response = DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        if (entities.Count == 0)
            return;

        Context.UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        foreach (var entity in entities)
        {
            entity.Deleted = true;
        }

        Context.UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        foreach (var entity in entities)
        {
            entity.Deleted = true;
        }

        Context.UpdateRange(entities);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AnyAsync(predicate);
    }
}