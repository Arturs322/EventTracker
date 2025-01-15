/// <summary>
/// Represents a company with details such as name, description, address, and contact information.
/// </summary>
public class CompanyDto
{
	/// <summary>
	/// Gets or sets the unique identifier for the company.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the company.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the description of the company. Optional.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets the address of the company.
	/// </summary>
	public string Address { get; set; }

	/// <summary>
	/// Gets or sets the phone number of the company.
	/// </summary>
	public string PhoneNumber { get; set; }

	/// <summary>
	/// Gets or sets the website URL of the company.
	/// </summary>
	public string Url { get; set; }
}
