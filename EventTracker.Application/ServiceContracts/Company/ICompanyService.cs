using EventTracker.Application.Dto;

namespace EventTracker.Application.ServiceContracts.Company
{
	/// <summary>
	/// Provides methods to manage company data.
	/// </summary>
	public interface ICompanyService
	{
		/// <summary>
		/// Retrieves a company by its ID.
		/// </summary>
		/// <param name="id">The ID of the company to retrieve.</param>
		/// <returns>The company details.</returns>
		Task<CompanyDto> GetCompany(int id);

		/// <summary>
		/// Retrieves all companies.
		/// </summary>
		/// <returns>A collection of companies.</returns>
		Task<IEnumerable<CompanyDto>> GetCompanies();

		/// <summary>
		/// Creates a new company.
		/// </summary>
		/// <param name="companyDto">The details of the company to create.</param>
		/// <returns>The result of the create operation.</returns>
		Task<ServiceResponseDto> CreateCompany(CompanyDto companyDto);

		/// <summary>
		/// Edits an existing company.
		/// </summary>
		/// <param name="id">The ID of the company to edit.</param>
		/// <param name="company">The updated company details.</param>
		/// <returns>The result of the edit operation.</returns>
		Task<ServiceResponseDto> EditCompany(int id, CompanyDto company);

		/// <summary>
		/// Deletes a company by its ID.
		/// </summary>
		/// <param name="id">The ID of the company to delete.</param>
		/// <returns>The result of the delete operation.</returns>
		Task<ServiceResponseDto> DeleteCompany(int id);
	}
}
