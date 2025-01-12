using EventTracker.Application.Dto;

namespace EventTracker.Application.ServiceContracts.Event
{
	/// <summary>
	/// Defines the contract for event-related operations.
	/// </summary>
	public interface IEventService
	{
		/// <summary>
		/// Retrieves the details of a specific event by its ID.
		/// </summary>
		/// <param name="id">The unique identifier of the event.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the event details as a <see cref="EventDto"/>.</returns>
		Task<EventDto> GetEvent(int id);

		/// <summary>
		/// Retrieves a collection of events based on optional filters.
		/// </summary>
		/// <param name="ageLimit">The optional age limit filter for the events.</param>
		/// <param name="categoryId">The optional category ID filter for the events.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a collection of events as <see cref="IEnumerable{EventDto}"/>.</returns>
		Task<IEnumerable<EventDto>> GetEvents(int? ageLimit, int? categoryId);

		/// <summary>
		/// Submits a new event to the system.
		/// </summary>
		/// <param name="eventObj">The event details to submit.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a response indicating the success or failure of the operation as a <see cref="ServiceResponseDto"/>.</returns>
		Task<ServiceResponseDto> SubmitEvent(EventDto eventObj);

		/// <summary>
		/// Edits an existing event in the system.
		/// </summary>
		/// <param name="eventObj">The updated event details.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a response indicating the success or failure of the operation as a <see cref="ServiceResponseDto"/>.</returns>
		Task<ServiceResponseDto> EditEvent(EventDto eventObj);

		/// <summary>
		/// Deletes an existing event by its ID.
		/// </summary>
		/// <param name="id">The unique identifier of the event to delete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a response indicating the success or failure of the operation as a <see cref="ServiceResponseDto"/>.</returns>
		Task<ServiceResponseDto> DeleteEvent(int id);

		/// <summary>
		/// Performs an action related to an event for a specific user, such as marking interest.
		/// </summary>
		/// <param name="eventId">The unique identifier of the event.</param>
		/// <param name="userId">The unique identifier of the user performing the action.</param>
		/// <param name="isInterested">A value indicating whether the user is interested in the event.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a response indicating the success or failure of the operation as a <see cref="ServiceResponseDto"/>.</returns>
		Task<ServiceResponseDto> EventAction(int eventId, string userId, bool isInterested);
	}
}
