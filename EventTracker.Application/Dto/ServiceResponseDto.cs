using EventTracker.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace EventTracker.Application.Dto;

/// <summary>
/// Data transfer object for service responses.
/// </summary>
public class ServiceResponseDto
{
	/// <summary>
	/// Gets or sets the message associated with the service response.
	/// </summary>
	public string? Message { get; set; }

	public MessageTypes MessageType { get; set; }

	/// <summary>
	/// Gets or sets the data associated with the service response.
	/// </summary>
	public dynamic? Data { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ServiceResponseDto"/> class.
	/// </summary>
	/// <param name="message">The message associated with the service response.</param>
	/// <param name="success">A flag indicating whether the operation represented by the response was successful.</param>
	/// <param name="data">The data associated with the service response.</param>
	/// <param name="status">The status to be set for the api endpoint.</param>
	public ServiceResponseDto(string? message = null, bool success = true, dynamic? data = null, int status = StatusCodes.Status200OK)
	{
		Message = message;
		MessageType = success ? MessageTypes.Success : MessageTypes.Error;
		Data = data;
		Status = status;
	}

	/// <summary>
	/// Gets a value indicating whether the operation represented by the response was successful.
	/// </summary>
	public bool Success => MessageType == MessageTypes.Success;

	/// <summary>
	/// Status to send back to the api endpoint for proper handling
	/// </summary>
	public int Status { get; set; } = StatusCodes.Status200OK;
}
