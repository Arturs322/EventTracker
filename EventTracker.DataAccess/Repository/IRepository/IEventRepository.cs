namespace EventTracker.DataAccess.Repository.IRepository
{
	public interface IEventRepository : IRepository<Event>
	{
		void Update(Event obj);
	}
}
