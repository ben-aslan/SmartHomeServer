using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public void Add(TEntity entity)
    {
        using (var context = new TContext())
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        using (var context = new TContext())
        {
            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        using (var context = new TContext())
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }

    public TEntity? GetOrDefault(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return context.Set<TEntity>()?.SingleOrDefault(filter);
        }
    }

    public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null!)
    {
        using (var context = new TContext())
        {
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        using (var context = new TContext())
        {
            context.Set<TEntity>().RemoveRange(entities);
            context.SaveChanges();
        }
    }

    public void Update(TEntity entity)
    {
        using (var context = new TContext())
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return context.Set<TEntity>().Single(filter);
        }
    }

    public bool Any(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return context.Set<TEntity>().AsNoTracking().Any(filter);
        }
    }





    public async Task AddAsync(TEntity entity)
    {
        using (var context = new TContext())
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync();
        }
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        using (var context = new TContext())
        {
            context.Set<TEntity>().AddRange(entities);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        using (var context = new TContext())
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }

    public async Task<TEntity?> GetOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return await context.Set<TEntity>()?.SingleOrDefaultAsync(filter)!;
        }
    }

    public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null!)
    {
        using (var context = new TContext())
        {
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        using (var context = new TContext())
        {
            context.Set<TEntity>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        using (var context = new TContext())
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return await context.Set<TEntity>().SingleAsync(filter);
        }
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return await context.Set<TEntity>().AsNoTracking().AnyAsync(filter);
        }
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return context.Set<TEntity>()?.FirstOrDefault(filter);
        }
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return await context.Set<TEntity>()?.FirstOrDefaultAsync(filter)!;
        }
    }

    public TEntity First(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return context.Set<TEntity>().First(filter);
        }
    }

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = new TContext())
        {
            return await context.Set<TEntity>().FirstAsync(filter);
        }
    }
}
