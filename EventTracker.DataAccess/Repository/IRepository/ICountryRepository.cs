using EventTracker.Models;

namespace EventTracker.DataAccess.Repository.IRepository
{
	public interface ICountryRepository : IRepository<Country>
	{
		void Update(Country obj);
	}
}
