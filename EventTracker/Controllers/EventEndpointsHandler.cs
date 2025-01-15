using EventTracker.Application.ServiceContracts.Event;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventTracker.Api.Controllers
{
	public static class EventEndpointsHandler
	{
		public static RouteGroupBuilder MapEventEndpoints(this RouteGroupBuilder group)
		{
			group.MapGet("/getEvent/{eventId:int}", GetEvent)
				.WithName(nameof(GetEvent))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets a specific event by provided ID"));

			group.MapGet("/getEvents/{ageLimit?}/{categoryId?}", GetEvents)
				.WithName(nameof(GetEvents))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Gets all events with possibility to filter by category and age limit"));

			group.MapPost("/submitEvent", SubmitEvent)
				.WithName(nameof(SubmitEvent))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new event"));

			group.MapPost("/editEvent/{eventId:int}", EditEvent)
				.WithName(nameof(EditEvent))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Edits an existing event"));

			group.MapPost("/deleteEvent/{eventId:int}", DeleteEvent)
				.WithName(nameof(DeleteEvent))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Removes an existing event"));

			group.MapPost("/eventAction/{eventId:int}", EventAction)
				.WithName(nameof(EventAction))
				.Produces(StatusCodes.Status200OK, contentType: "application/json")
				.ProducesProblem(StatusCodes.Status404NotFound)                
				.WithMetadata(new SwaggerOperationAttribute(summary: "Sets an 'interested' flag for an event"));

			return group;
		}

		public static void MapEventEndpoints(this WebApplication app)
		{
			app.MapGroup("api/event")
				.MapEventEndpoints()
				.WithTags("Event");
		}

		public static async Task<IResult> GetEvent(
			[FromRoute, SwaggerParameter("Event ID")] int eventId,
			IEventService eventService,
			CancellationToken cancellation)
		{
			try
			{
				var result = await eventService.GetEvent(eventId, cancellation);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> GetEvents(
			IEventService eventService,
			[FromQuery, SwaggerParameter("Age limit for the event")] int? ageLimit,
			[FromQuery, SwaggerParameter("Category Id for the event")] int? categoryId,
			CancellationToken cancellation)
		{
			try
			{
				var result = await eventService.GetEvents(cancellation, ageLimit, categoryId);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> SubmitEvent(
			IEventService eventService,
			[FromBody, SwaggerParameter("EventDto")] EventDto eventObj,
			CancellationToken cancellation)
		{
			try
			{
				var result = await eventService.SubmitEvent(eventObj, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> EditEvent(
			IEventService eventService,
			[FromRoute, SwaggerParameter("Event ID")] int eventId,
			[FromBody, SwaggerParameter("EventDto")] EventDto eventObj,
			CancellationToken cancellation)
		{
			try
			{
				var result = await eventService.EditEvent(eventId, eventObj, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> DeleteEvent(
			IEventService eventService,
			[FromRoute, SwaggerParameter("Event ID")] int eventId,
			[FromBody, SwaggerParameter("EventDto")] EventDto eventObj,
			CancellationToken cancellation)
		{
			try
			{
				var result = await eventService.DeleteEvent(eventId, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

		public static async Task<IResult> EventAction(
			HttpContext httpContext,
			IEventService eventService,
			[FromRoute, SwaggerParameter("Event ID")] int eventId,
			[FromBody, SwaggerParameter("EventDto")] EventDto eventObj,
			CancellationToken cancellation)
		{
			try
			{
				var userId = httpContext.User.Identity.Name;
				var result = await eventService.EventAction(eventId, userId, true, cancellation);

				return result.Success ? Results.Ok() : Results.Problem(result.Message);
			}
			catch (Exception ex)
			{
				return Results.Problem(new ProblemDetails { Title = "Server Error", Detail = ex.Message, Status = StatusCodes.Status500InternalServerError });
			}
		}

	}
}
