using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<TKey, TEntity> where TEntity : Entity<TKey>
    {
        protected DbSet<TEntity> Set { get; set; }
        protected DbContext Context { get; set; }

        public Repository(DbContext context)
        {
            this.Context = context;
            Set = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        )
        {
            IQueryable<TEntity> query = Set;
            if (filter != null) query = query.Where(filter);
            if (orderBy != null) orderBy(query).ToList();
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetEntityAsync(object key)
        {
            return (await GetEntitiesAsync(e => e.Id.Equals(key))).FirstOrDefault();
        }

        public virtual async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            return (await Set.AddAsync(entity)).Entity;
        }
        public virtual async Task<TEntity> DeleteEntityAsync(object key)
        {
            TEntity entityToDelete = await GetEntityAsync(key);
            Set.Remove(entityToDelete);
            return entityToDelete;
        }

        public async Task DeleteEntitiesAsync(Expression<Func<TEntity, bool>> filter)
        {
            Set.RemoveRange(await GetEntitiesAsync(filter));
        }

        public void DeleteEntity(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
        }

        public virtual void UpdateEntity(TEntity entity)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task UpdateEntitiesAsync(
            Expression<Func<TEntity, bool>> filter,
            Action<TEntity> action
        )
        {
            IQueryable<TEntity> query = Set;
            (await query.Where(filter).ToListAsync()).ForEach(action);
            Set.UpdateRange(query);
        }

        public virtual void InsertEntities(IEnumerable<TEntity> entities)
        {
            Set.AddRange(entities);
        }

        public virtual async Task<bool> EntityExistAsync(TKey id)
        {
            return await GetEntityAsync(id) != null;
        }
    }
}
