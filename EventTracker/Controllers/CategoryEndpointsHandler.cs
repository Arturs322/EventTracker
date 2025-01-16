using EventTracker.Application.ServiceContracts.Category;
using EventTracker.Application.ServiceContracts.Event;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventTracker.Api.Controllers
{
	public static class CategoryEndpointsHandler
	{
		public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
		{
			group.MapGet("/getCategory/{categoryId:int}", GetCategory)
				.WithName(nameof(GetCategory))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets a specific category by provided ID"));

			group.MapGet("/getCategories", GetCategories)
				.WithName(nameof(GetCategories))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets all categories"));

			group.MapPost("/createCategory", CreateCategory)
				.WithName(nameof(CreateCategory))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new category"));

			group.MapPost("/editCategory/{categoryId:int}", EditCategory)
				.WithName(nameof(EditCategory))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Edits an existing category"));

			group.MapPost("/deleteCategory/{categoryId:int}", DeleteCategory)
				.WithName(nameof(DeleteCategory))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Removes an existing category"));

			return group;
		}

		public static void MapCategoryEndpoints(this WebApplication app)
		{
			app.MapGroup("api/category")
				.MapCategoryEndpoints()
				.WithTags("Category");
		}

		public static async Task<IResult> GetCategory(
			[FromRoute, SwaggerParameter("Category ID")] int categoryId,
			ICategoryService categoryService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await categoryService.GetCategory(categoryId, cancellation);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> GetCategories(
			ICategoryService categoryService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await categoryService.GetCategories(cancellation);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> CreateCategory(
			ICategoryService categoryService,
			[FromBody, SwaggerParameter("CategoryDto")] CategoryDto categoryDto,
			CancellationToken cancellation)
		{
			try
			{
				var result = await categoryService.CreateCategory(categoryDto, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> EditCategory(
			ICategoryService categoryService,
			[FromRoute, SwaggerParameter("Category ID")] int categoryId,
			[FromBody, SwaggerParameter("CategoryDto")] CategoryDto categoryDto,
			CancellationToken cancellation)
		{
			try
			{
				var result = await categoryService.EditCategory(categoryId, categoryDto, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> DeleteCategory(
			ICategoryService categoryService,
			[FromRoute, SwaggerParameter("Category ID")] int categoryId,
			CancellationToken cancellation)
		{
			try
			{
				var result = await categoryService.DeleteCategory(categoryId, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}
	}
}
