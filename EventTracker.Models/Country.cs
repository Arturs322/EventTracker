using System.ComponentModel.DataAnnotations;

namespace EventTracker.Models
{
	public class Country
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? CountryCode { get; set; }
	}
}
