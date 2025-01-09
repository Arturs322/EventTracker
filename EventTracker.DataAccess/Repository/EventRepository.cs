using EventTracker.DataAccess.Repository.IRepository;

namespace EventTracker.DataAccess.Repository
{
	public class EventRepository : Repository<Event>, IEventRepository
	{
		private ApplicationDbContext _db;

		public EventRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


		public void Update(Event obj)
		{
			_db.Events.Update(obj);
		}
	}
}
