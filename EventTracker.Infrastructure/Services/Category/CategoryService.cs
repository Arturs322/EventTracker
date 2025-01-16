using AutoMapper;
using EventTracker.Application.Dto;
using EventTracker.Application.ServiceContracts.Category;
using EventTracker.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Logging;

namespace EventTracker.Infrastructure.Services.Category
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<CategoryService> _logger;

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}

		/// <inheritdoc />
		public async Task<CategoryDto> GetCategory(int id, CancellationToken cancellation)
		{
			try
			{
				var category = await _unitOfWork.Category.GetByIdAsync(id);
				return _mapper.Map<CategoryDto>(category);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<CategoryDto>> GetCategories(CancellationToken cancellation)
		{
			try
			{
				var categories = await _unitOfWork.Category.GetAllAsync();
				return _mapper.Map<IEnumerable<CategoryDto>>(categories);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> CreateCategory(CategoryDto categoryDto, CancellationToken cancellation)
		{
			try
			{
				var category = new EventTracker.Models.Category
				{
					Title = categoryDto.Title
				};

				await _unitOfWork.Category.AddAsync(category);
				_unitOfWork.Save();

				return new ServiceResponseDto("Category added successfully;");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> EditCategory(int id, CategoryDto category, CancellationToken cancellation)
		{
			try
			{
				var currentCategory = await _unitOfWork.Category.GetByIdAsync(id);
				if (currentCategory == null)
				{
					return new ServiceResponseDto("Could not find a category!", false);
				}

				currentCategory.Title = category.Title;

				await _unitOfWork.Category.UpdateAsync(currentCategory);
				_unitOfWork.Save();

				return new ServiceResponseDto("Category edited successfully!");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}

		/// <inheritdoc />
		public async Task<ServiceResponseDto> DeleteCategory(int id, CancellationToken cancellation)
		{
			try
			{
				var category = await _unitOfWork.Category.GetByIdAsync(id);
				if (category == null)
				{
					return new ServiceResponseDto("Could not find a category!", false);
				}

				await _unitOfWork.Category.RemoveAsync(category);
				_unitOfWork.Save();

				return new ServiceResponseDto("Category deleted succesfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}
		}
	}
}
