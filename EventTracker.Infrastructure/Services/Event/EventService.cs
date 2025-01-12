using AutoMapper;
using EventTracker.Application.Dto;
using EventTracker.Application.ServiceContracts.Event;
using EventTracker.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Logging;

namespace EventTracker.Infrastructure.Services.Event
{
	public class EventService : IEventService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<EventService> _logger;

		public EventService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EventService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		/// <inheritdoc />
		public async Task<EventDto> GetEvent(int id)
		{
			try
			{
				var singleEvent = await _unitOfWork.Event.GetByIdAsync(id);
				return _mapper.Map<EventDto>(singleEvent);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<EventDto>> GetEvents(int? ageLimit, int? categoryId)
		{
			try
			{
				var validAgeLimit = ageLimit ?? 0;
				var validCategoryId = categoryId ?? 0;

				var events = await _unitOfWork.Event.GetAllIncludingAsync(
					u => (validCategoryId == 0 || u.CategoryId == validCategoryId) &&
						 (validAgeLimit == 0 || u.AgeLimit <= validAgeLimit)
				);

				return _mapper.Map<IEnumerable<EventDto>>(events);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> SubmitEvent(EventDto eventObj)
		{
			try
			{
				var newEvent = new EventTracker.Models.Event()
				{
					Title = eventObj.Title,
					Description = eventObj.Description,
					DateFrom = eventObj.DateFrom,
					DateTo = eventObj.DateTo,
					AgeLimit = eventObj.AgeLimit,
					TicketPrice = eventObj.TicketPrice,
					CategoryId = eventObj.CategoryId,
				};

				await _unitOfWork.Event.AddAsync(newEvent);
				_unitOfWork.Save();

				return new ServiceResponseDto("Event submitted sucessfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> EditEvent(EventDto eventObj)
		{
			try
			{
				var currentEvent = await _unitOfWork.Event.GetByIdAsync(eventObj.Id);

				if (currentEvent == null)
				{
					return new ServiceResponseDto("Could not find an event!", false);
				}

				currentEvent.Title = eventObj.Title;
				currentEvent.Description = eventObj.Description;
				currentEvent.DateFrom = eventObj.DateFrom;
				currentEvent.DateTo = eventObj.DateTo;
				currentEvent.AgeLimit = eventObj.AgeLimit;
				currentEvent.CategoryId = eventObj.CategoryId;
				currentEvent.TicketPrice = eventObj.TicketPrice;

				await _unitOfWork.Event.UpdateAsync(currentEvent);
				_unitOfWork.Save();

				return new ServiceResponseDto("Event edited sucessfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> DeleteEvent(int id)
		{
			try
			{
				var eventItem = await _unitOfWork.Event.GetByIdAsync(id);
				if (eventItem == null)
				{
					return new ServiceResponseDto("Could not find an event!", false);
				}

				await _unitOfWork.Event.RemoveAsync(eventItem);
				_unitOfWork.Save();

				return new ServiceResponseDto("Event deleted sucessfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		//TODO implement relational table event - user interest count
		/// <inheritdoc />
		public async Task<ServiceResponseDto> EventAction(int eventId, string userId, bool isInterested)
		{
			try
			{
				var eventItem = await _unitOfWork.Event.GetByIdAsync(eventId);
				if (eventItem == null)
				{
					return new ServiceResponseDto("Could not find an event!", false);
				}

				var user = await _unitOfWork.ApplicationUser.GetSingleIncludingAsync(u => u.Id == userId);
				if (user == null)
				{
					return new ServiceResponseDto("Could not find user!", false);
				}

				if (isInterested)
				{
					eventItem.Interested++;
				}

				await _unitOfWork.Event.UpdateAsync(eventItem);
				_unitOfWork.Save();

				return new ServiceResponseDto("Event updated sucessfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}
	}
}
