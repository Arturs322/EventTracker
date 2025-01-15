using EventTracker.Application.Dto;

namespace EventTracker.Application.ServiceContracts.Country
{
	/// <summary>
	/// Provides methods to manage country data.
	/// </summary>
	public interface ICountryService
	{
		/// <summary>
		/// Retrieves a country by its ID.
		/// </summary>
		/// <param name="id">The ID of the country to retrieve.</param>
		/// <returns>The country details.</returns>
		Task<CountryDto> GetCountry(int id);

		/// <summary>
		/// Retrieves all countries.
		/// </summary>
		/// <returns>A collection of countries.</returns>
		Task<IEnumerable<CountryDto>> GetCountries();

		/// <summary>
		/// Creates a new country.
		/// </summary>
		/// <param name="countryDto">The details of the country to create.</param>
		/// <returns>The result of the create operation.</returns>
		Task<ServiceResponseDto> CreateCountry(CountryDto countryDto);

		/// <summary>
		/// Edits an existing country.
		/// </summary>
		/// <param name="id">The ID of the country to edit.</param>
		/// <param name="country">The updated country details.</param>
		/// <returns>The result of the edit operation.</returns>
		Task<ServiceResponseDto> EditCountry(int id, CountryDto country);

		/// <summary>
		/// Deletes a country by its ID.
		/// </summary>
		/// <param name="id">The ID of the country to delete.</param>
		/// <returns>The result of the delete operation.</returns>
		Task<ServiceResponseDto> DeleteCountry(int id);
	}
}
