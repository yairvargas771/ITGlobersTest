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
        public DbSet<TEntity> Set { get; set; }
        public DbContext Context { get; set; }

        public Repository(DbContext context)
        {
            this.Context = context;
            Set = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntities(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
        )
        {
            IQueryable<TEntity> query = Set;
            if (filter != null) query = query.Where(filter);
            if (orderBy != null) orderBy(query).ToList();
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetEntity(object key)
        {
            return (await GetEntities(e => e.Id.Equals(key))).FirstOrDefault();
        }

        public virtual void InsertEntity(TEntity entity)
        {
            Set.Add(entity);
        }
    }
}
