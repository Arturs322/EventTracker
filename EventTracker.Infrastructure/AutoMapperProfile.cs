using AutoMapper;
using EventTracker.Application.Dto;
using EventTracker.Models;

namespace EventTracker.Infrastructure
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Event, EventDto>().ReverseMap();
		}
	}
}
