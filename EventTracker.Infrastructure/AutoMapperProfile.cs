using AutoMapper;
using EventTracker.Models;

namespace EventTracker.Infrastructure
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Event, EventDto>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<Company, CompanyDto>().ReverseMap();
			CreateMap<Country, CountryDto>().ReverseMap();
		}
	}
}
