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

        private void Include(Queue<string> includes, ref IQueryable<TEntity> query)
        {
            if (includes.Count == 0)
                return;
            query = query.Include(includes.Dequeue());
            Include(includes, ref query);
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string include = "",
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IQueryable<TEntity> query = null
        )
        {
            if (query == null)
                query = Set;
            if (filter != null) query = query.Where(filter);
            if (orderBy != null) orderBy(query).ToList();
            if (include.Length > 0) Include(new Queue<string>(include.Split(",")), ref query);
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetEntityAsync(object key, string include = "", IQueryable<TEntity> query = null)
        {
            return (await GetEntitiesAsync(e => e.Id.Equals(key), include, null, query)).FirstOrDefault();
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
