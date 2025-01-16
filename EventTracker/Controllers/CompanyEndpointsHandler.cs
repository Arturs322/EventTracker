using EventTracker.Application.ServiceContracts.Company;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventTracker.Api.Controllers
{
	public static class CompanyEndpointsHandler
	{
		public static RouteGroupBuilder MapCompanyEndpoints(this RouteGroupBuilder group)
		{
			group.MapGet("/getCompany/{companyId:int}", GetCompany)
				.WithName(nameof(GetCompany))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets a specific company by the provided ID"));

			group.MapGet("/getCompanies", GetCompanies)
				.WithName(nameof(GetCompanies))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets all companies"));

			group.MapPost("/createCompany", CreateCompany)
				.WithName(nameof(CreateCompany))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new company"));

			group.MapPost("/editCompany/{companyId:int}", EditCompany)
				.WithName(nameof(EditCompany))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Edits an existing company"));

			group.MapPost("/deleteCompany/{companyId:int}", DeleteCompany)
				.WithName(nameof(DeleteCompany))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithMetadata(new SwaggerOperationAttribute(summary: "Deletes an existing company"));

			return group;
		}

		public static void MapCompanyEndpoints(this WebApplication app)
		{
			app.MapGroup("api/company")
				.MapCompanyEndpoints()
				.WithTags("Company");
		}

		public static async Task<IResult> GetCompany(
			[FromRoute, SwaggerParameter("Company ID")] int companyId,
			ICompanyService companyService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await companyService.GetCompany(companyId);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> GetCompanies(
			ICompanyService companyService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await companyService.GetCompanies();

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> CreateCompany(
			ICompanyService companyService,
			[FromBody, SwaggerParameter("CompanyDto")] CompanyDto companyDto,
			CancellationToken cancellation)
		{
			try
			{
				var result = await companyService.CreateCompany(companyDto);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> EditCompany(
			ICompanyService companyService,
			[FromRoute, SwaggerParameter("Company ID")] int companyId,
			[FromBody, SwaggerParameter("CompanyDto")] CompanyDto companyDto,
			CancellationToken cancellation)
		{
			try
			{
				var result = await companyService.EditCompany(companyId, companyDto);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> DeleteCompany(
			ICompanyService companyService,
			[FromRoute, SwaggerParameter("Company ID")] int companyId,
			CancellationToken cancellation)
		{
			try
			{
				var result = await companyService.DeleteCompany(companyId);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}
	}
}
