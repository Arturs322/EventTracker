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
	}
}
