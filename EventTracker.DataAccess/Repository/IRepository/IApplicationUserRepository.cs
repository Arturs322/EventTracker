using EventTracker.Models;

namespace EventTracker.DataAccess.Repository.IRepository
{
	public interface IApplicationUserRepository : IRepository<ApplicationUser>
	{
		void Update(ApplicationUser obj);
	}
}
