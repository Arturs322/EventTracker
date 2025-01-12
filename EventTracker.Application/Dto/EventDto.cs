namespace EventTracker.Application.Dto
{
	public class EventDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public int? Interested { get; set; } = 0;
		public double? TicketPrice { get; set; } = 0;
		public int AgeLimit { get; set; }
		public int CategoryId { get; set; }
		public CategoryDto Category { get; set; }
	}
}
