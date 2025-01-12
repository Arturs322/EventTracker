using EventTracker.Models;

namespace EventTracker.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository Category { get; }
		ICompanyRepository Company { get; }
		ICountryRepository Country { get; }
		IApplicationUserRepository ApplicationUser { get; }
		IEventRepository Event { get; }

		/// <summary>
		/// Commits all changes made in this context to the database synchronously.
		/// </summary>
		void Save();

		/// <summary>
		/// Commits all changes made in this context to the database asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous save operation.</returns>
		Task SaveAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Detaches the specified entity from the DbContext, meaning it will no longer be tracked by Entity Framework.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity to detach.</typeparam>
		/// <param name="entity">The entity to detach from tracking.</param>
		void DetachEntity<TEntity>(TEntity entity) where TEntity : class;
	}
}
