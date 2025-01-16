using EventTracker.Application.Dto;

namespace EventTracker.Application.ServiceContracts.Category
{
	/// <summary>
	/// Provides methods to manage category data.
	/// </summary>
	public interface ICategoryService
	{
		/// <summary>
		/// Retrieves a category by its ID.
		/// </summary>
		/// <param name="id">The ID of the category to retrieve.</param>
		/// <returns>The category details.</returns>
		Task<CategoryDto> GetCategory(int id, CancellationToken cancellation);

		/// <summary>
		/// Retrieves all categories.
		/// </summary>
		/// <returns>A collection of categories.</returns>
		Task<IEnumerable<CategoryDto>> GetCategories(CancellationToken cancellation);

		/// <summary>
		/// Creates a new category.
		/// </summary>
		/// <param name="categoryDto">The details of the category to create.</param>
		/// <returns>The result of the create operation.</returns>
		Task<ServiceResponseDto> CreateCategory(CategoryDto categoryDto, CancellationToken cancellation);

		/// <summary>
		/// Edits an existing category.
		/// </summary>
		/// <param name="id">The ID of the category to edit.</param>
		/// <param name="category">The updated category details.</param>
		/// <returns>The result of the edit operation.</returns>
		Task<ServiceResponseDto> EditCategory(int id, CategoryDto categoryDto, CancellationToken cancellation);

		/// <summary>
		/// Deletes a category by its ID.
		/// </summary>
		/// <param name="id">The ID of the category to delete.</param>
		/// <returns>The result of the delete operation.</returns>
		Task<ServiceResponseDto> DeleteCategory(int id, CancellationToken cancellation);
	}
}
