/// <summary>
/// Represents an application user with personal details and an image.
/// </summary>
public class ApplicationUserDto
{
	/// <summary>
	/// Gets or sets the first name of the user. Optional.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the surname of the user. Optional.
	/// </summary>
	public string? Surname { get; set; }

	/// <summary>
	/// Gets or sets the profile image of the user. Optional.
	/// </summary>
	public string? Image { get; set; }
}
