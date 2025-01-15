using EventTracker.Application.Dto;

/// <summary>
/// Represents an event with details such as title, description, date, and category.
/// </summary>
public class EventDto
{
	/// <summary>
	/// Gets or sets the unique identifier for the event.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the title of the event.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Gets or sets the description of the event. Optional.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets the starting date and time of the event.
	/// </summary>
	public DateTime DateFrom { get; set; }

	/// <summary>
	/// Gets or sets the ending date and time of the event.
	/// </summary>
	public DateTime DateTo { get; set; }

	/// <summary>
	/// Gets or sets the number of people interested in the event. Defaults to 0.
	/// </summary>
	public int? Interested { get; set; } = 0;

	/// <summary>
	/// Gets or sets the ticket price for the event. Defaults to 0.
	/// </summary>
	public double? TicketPrice { get; set; } = 0;

	/// <summary>
	/// Gets or sets the age limit for the event.
	/// </summary>
	public int AgeLimit { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier of the category to which the event belongs.
	/// </summary>
	public int CategoryId { get; set; }

	/// <summary>
	/// Gets or sets the category details of the event.
	/// </summary>
	public CategoryDto Category { get; set; }
}
