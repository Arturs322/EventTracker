using AutoMapper;
using EventTracker.Application.Dto;
using EventTracker.Application.ServiceContracts.Country;
using EventTracker.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Logging;

namespace EventTracker.Infrastructure.Services.Country
{
	public class CountryService : ICountryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<CountryService> _logger;

		public CountryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CountryService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		/// <inheritdoc />
		public async Task<CountryDto> GetCountry(int id)
		{
			try
			{
				var country = await _unitOfWork.Country.GetByIdAsync(id);
				return _mapper.Map<CountryDto>(country);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<CountryDto>> GetCountries()
		{
			try
			{
				var countries = await _unitOfWork.Country.GetAllAsync();
				return _mapper.Map<IEnumerable<CountryDto>>(countries);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> CreateCountry(CountryDto countryDto)
		{
			try
			{
				var category = new Models.Country
				{
					Name = countryDto.Name,
					CountryCode = countryDto.CountryCode,
				};

				await _unitOfWork.Country.AddAsync(category);
				_unitOfWork.Save();

				return new ServiceResponseDto("Country added successfully;");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> EditCountry(int id, CountryDto country)
		{
			try
			{
				var currentCountry = await _unitOfWork.Country.GetByIdAsync(id);
				if (currentCountry == null)
				{
					return new ServiceResponseDto("Could not find a country!", false);
				}

				currentCountry.Name = country.Name;
				currentCountry.CountryCode = country.CountryCode;

				await _unitOfWork.Country.UpdateAsync(currentCountry);
				_unitOfWork.Save();

				return new ServiceResponseDto("Country edited successfully!");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> DeleteCountry(int id)
		{
			try
			{
				var country = await _unitOfWork.Country.GetByIdAsync(id);
				if (country == null)
				{
					return new ServiceResponseDto("Could not find a country!", false);
				}

				await _unitOfWork.Country.RemoveAsync(country);
				_unitOfWork.Save();

				return new ServiceResponseDto("Country deleted succesfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}
	}
}
