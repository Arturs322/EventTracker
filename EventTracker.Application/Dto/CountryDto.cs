/// <summary>
/// Represents a country with details such as name and country code.
/// </summary>
public class CountryDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the country.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the country code. Optional.
    /// </summary>
    public string? CountryCode { get; set; }
}
