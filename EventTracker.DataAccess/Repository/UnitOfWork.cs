using EventTracker.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EventTracker.DataAccess.Repository
{

	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Company = new CompanyRepository(_db);
			Country = new CountryRepository(_db);
			Event = new EventRepository(_db);
			ApplicationUser = new ApplicationUserRepository(_db);
		}

		public ICategoryRepository Category { get; private set; }
		public ICompanyRepository Company { get; private set; }
		public ICountryRepository Country { get; private set; }
		public IEventRepository Event { get; private set; }
		public IApplicationUserRepository ApplicationUser { get; private set; }

		/// <inheritdoc/>
		public void Save()
		{
			_db.SaveChanges();
		}

		/// <inheritdoc/>
		public async Task SaveAsync(CancellationToken cancellationToken = default)
		{
			await _db.SaveChangesAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public void DetachEntity<TEntity>(TEntity entity) where TEntity : class
		{
			_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
