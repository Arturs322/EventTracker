using Microsoft.EntityFrameworkCore;
using EventTracker.DataAccess.Repository.IRepository;
using System.Linq.Expressions;

namespace EventTracker.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<T> query = dbSet;

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return await query.FirstOrDefaultAsync();
            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return await query.FirstOrDefaultAsync();
            }

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetSingleIncludingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetSingleIncludingAsync(predicate, dbSet, includeProperties);
        }
        
        public async Task<T> GetSingleNonTrackedIncludingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetSingleIncludingAsync(predicate, dbSet.AsNoTracking(), includeProperties);
        }

        private async Task<T> GetSingleIncludingAsync(Expression<Func<T, bool>> predicate, IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }
        
        public async Task RemoveWhereAsync(Expression<Func<T, bool>> filter)
        {
            await RemoveRangeAsync(dbSet.Where(filter));
        }

        public async Task UpdateSpecialAsync(int entityId, T entity, params string[] fieldsToUpdate)
        {
            var dbEntity = await dbSet.FindAsync(entityId);
            if (dbEntity == null)
            {
                throw new Exception("Entity not found");
            }

            var dtoType = typeof(T);
            var entityType = typeof(DbSet<T>);

            foreach (var field in fieldsToUpdate)
            {
                var dtoProperty = dtoType.GetProperty(field);
                var entityProperty = entityType.GetProperty(field);

                if (dtoProperty != null && entityProperty != null && entityProperty.CanWrite)
                {
                    var value = dtoProperty.GetValue(entity);
                    entityProperty.SetValue(dbEntity, value);
                }
            }
            dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

    }
}
