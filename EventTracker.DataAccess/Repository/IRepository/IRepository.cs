using System.Linq.Expressions;

namespace EventTracker.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string? includeProperties = null);
		Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties);
		Task<T> GetSingleIncludingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
		Task<T> GetSingleNonTrackedIncludingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		Task UpdateAsync(T entity);
		Task RemoveAsync(T entity);
		Task RemoveRangeAsync(IEnumerable<T> entities);
		Task RemoveWhereAsync(Expression<Func<T, bool>> filter);
		Task UpdateSpecialAsync(int entityId, T entity, params string[] fieldsToUpdate);
		Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
	}
}
