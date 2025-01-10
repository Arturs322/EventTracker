using EventTracker.Models;

namespace EventTracker.DataAccess.Repository.IRepository
{
	public interface ICompanyRepository : IRepository<Company>
	{
		void Update(Company obj);
	}
}
