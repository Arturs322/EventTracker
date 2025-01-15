using AutoMapper;
using EventTracker.Application.Dto;
using EventTracker.Application.ServiceContracts.Company;
using EventTracker.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Logging;

namespace EventTracker.Infrastructure.Services.Company
{
	public class CompanyService : ICompanyService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<CompanyService> _logger;

		public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompanyService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		/// <inheritdoc />
		public async Task<CompanyDto> GetCompany(int id)
		{
			try
			{
				var company = await _unitOfWork.Company.GetByIdAsync(id);
				return _mapper.Map<CompanyDto>(company);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<CompanyDto>> GetCompanies()
		{
			try
			{
				var companies = await _unitOfWork.Company.GetAllAsync();
				return _mapper.Map<IEnumerable<CompanyDto>>(companies);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> CreateCompany(CompanyDto companyDto)
		{
			try
			{
				var category = new Models.Company
				{
					Name = companyDto.Name,
					Description = companyDto.Description,
					Address = companyDto.Address,
					PhoneNumber = companyDto.PhoneNumber,
					Url = companyDto.Url,
				};

				await _unitOfWork.Company.AddAsync(category);
				_unitOfWork.Save();

				return new ServiceResponseDto("Company added successfully;");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> EditCompany(int id, CompanyDto company)
		{
			try
			{
				var currentCompany = await _unitOfWork.Company.GetByIdAsync(id);
				if (currentCompany == null)
				{
					return new ServiceResponseDto("Could not find a company!", false);
				}

				currentCompany.Name = company.Name;
				currentCompany.Description = company.Description;
				currentCompany.Address = company.Address;
				currentCompany.PhoneNumber = company.PhoneNumber;
				currentCompany.Url = company.Url;

				await _unitOfWork.Company.UpdateAsync(currentCompany);
				_unitOfWork.Save();

				return new ServiceResponseDto("Company edited successfully!");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> DeleteCompany(int id)
		{
			try
			{
				var company = await _unitOfWork.Company.GetByIdAsync(id);
				if (company == null)
				{
					return new ServiceResponseDto("Could not find a company!", false);
				}

				await _unitOfWork.Company.RemoveAsync(company);
				_unitOfWork.Save();

				return new ServiceResponseDto("Company deleted succesfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}
	}
}
