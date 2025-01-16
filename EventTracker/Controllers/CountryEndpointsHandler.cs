using EventTracker.Application.ServiceContracts.Country;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventTracker.Api.Controllers
{
	public static class CountryEndpointsHandler
	{
		public static RouteGroupBuilder MapCountryEndpoints(this RouteGroupBuilder group)
		{
			group.MapGet("/getCountry/{countryId:int}", GetCountry)
				.WithName(nameof(GetCountry))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets a specific country by the provided ID"));

			group.MapGet("/getCountries", GetCountries)
				.WithName(nameof(GetCountries))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets all countries"));

			group.MapPost("/createCountry", CreateCountry)
				.WithName(nameof(CreateCountry))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new country"));

			group.MapPost("/editCountry/{countryId:int}", EditCountry)
				.WithName(nameof(EditCountry))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Edits an existing country"));

			group.MapPost("/deleteCountry/{countryId:int}", DeleteCountry)
				.WithName(nameof(DeleteCountry))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Deletes an existing country"));

			return group;
		}

		public static void MapCountryEndpoints(this WebApplication app)
		{
			app.MapGroup("api/country")
				.MapCountryEndpoints()
				.WithTags("Country");
		}

		public static async Task<IResult> GetCountry(
			[FromRoute, SwaggerParameter("Country ID")] int countryId,
			ICountryService countryService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await countryService.GetCountry(countryId);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> GetCountries(
			ICountryService countryService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await countryService.GetCountries();

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> CreateCountry(
			ICountryService countryService,
			[FromBody, SwaggerParameter("CountryDto")] CountryDto countryDto,
			CancellationToken cancellation)
		{
			try
			{
				var result = await countryService.CreateCountry(countryDto);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> EditCountry(
			ICountryService countryService,
			[FromRoute, SwaggerParameter("Country ID")] int countryId,
			[FromBody, SwaggerParameter("CountryDto")] CountryDto countryDto,
			CancellationToken cancellation)
		{
			try
			{
				var result = await countryService.EditCountry(countryId, countryDto);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> DeleteCountry(
			ICountryService countryService,
			[FromRoute, SwaggerParameter("Country ID")] int countryId,
			CancellationToken cancellation)
		{
			try
			{
				var result = await countryService.DeleteCountry(countryId);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}
	}
}
