using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts.Repositories;

using Application.Entities;
using Application.Infrastructure.Data.Queries;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly ApplicationDbContext DbContext;

    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    // Read operations
    public TEntity? GetById(Guid id)
    {
        return DbContext.Set<TEntity>().Find(id);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return DbContext.Set<TEntity>().ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public PagedList<TEntity> GetPaged(PagedQuery query)
    {
        var totalCount = Count();
        var items = DbContext.Set<TEntity>()
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedList<TEntity>(items, totalCount, query.PageNumber, query.PageSize);
    }

    public async Task<PagedList<TEntity>> GetPagedAsync(PagedQuery query)
    {
        var totalCount = await CountAsync();
        var items = await DbContext.Set<TEntity>()
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedList<TEntity>(items, totalCount, query.PageNumber, query.PageSize);
    }

    public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>().Where(predicate).ToList();
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public PagedList<TEntity> SearchPaged(Expression<Func<TEntity, bool>> predicate, PagedQuery query)
    {
        var totalCount = Count(predicate);
        var items = DbContext.Set<TEntity>()
            .Where(predicate)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedList<TEntity>(items, totalCount, query.PageNumber, query.PageSize);
    }

    public async Task<PagedList<TEntity>> SearchPagedAsync(Expression<Func<TEntity, bool>> predicate, PagedQuery query)
    {
        var totalCount = await CountAsync(predicate);
        var items = await DbContext.Set<TEntity>()
            .Where(predicate)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedList<TEntity>(items, totalCount, query.PageNumber, query.PageSize);
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>().FirstOrDefault(predicate);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>().Any(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbContext.Set<TEntity>().AnyAsync(predicate);
    }

    public int Count(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? DbContext.Set<TEntity>().Count() : DbContext.Set<TEntity>().Count(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? await DbContext.Set<TEntity>().CountAsync() : await DbContext.Set<TEntity>().CountAsync(predicate);
    }

    // Write operations
    public TEntity Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public TEntity AddRange(IEnumerable<TEntity> entities)
    {
        DbContext.Set<TEntity>().AddRange(entities);
        return entities.First();
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities);
        return entities;
    }

    public TEntity Update(TEntity entity)
    {
        DbContext.Update(entity);
        return entity;
    }

    public bool Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
        return true;
    }

    public bool RemoveRange(IEnumerable<TEntity> entities)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);
        return true;
    }

    // Unit of work
    public int SaveChanges()
    {
        return DbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}