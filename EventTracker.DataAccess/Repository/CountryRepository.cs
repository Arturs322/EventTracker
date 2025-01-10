using EventTracker.DataAccess.Repository.IRepository;
using EventTracker.Models;

namespace EventTracker.DataAccess.Repository
{
	public class CountryRepository : Repository<Country>, ICountryRepository
	{
		private ApplicationDbContext _db;

		public CountryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


		public void Update(Country obj)
		{
			_db.Countries.Update(obj);
		}
	}
}
